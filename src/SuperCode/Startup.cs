using System;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ElectronNET.API;
using ElectronNET.API.Entities;
using SuperCode.Configs;
using SuperCode.Db;
using Element;
using SuperCode.Utils;

namespace SuperCode
{
    public class Startup
    { 
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddHttpClient();
            services.AddServerSideBlazor();

            services.AddElementServices();
            GlobalElementSettings.DisableAnimation = true;
            GlobalElementSettings.Size = Element.Size.Mini;

            services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(sp.GetService<NavigationManager>().BaseUri)
            });

            services.AddSuperCodeServices();

            services.Configure<DbConfig>(Configuration.GetSection("DbConfig"));

            var codeSettings = new ConfigHelper().Load("codesettings", AppContext.BaseDirectory, Environment.EnvironmentName, true);
            services.Configure<CodeSettings>(codeSettings);

            //���ݿ�
            services.AddDbAsync().Wait();
            Console.WriteLine("\n");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            if (HybridSupport.IsElectronActive)
            {
                ElectronSuperCode(env);
            }
        }

        public async void ElectronSuperCode(IWebHostEnvironment env)
        {
            var mainWindow = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
            {
                Title = "SuperCode",
                Icon = AppContext.BaseDirectory + "/wwwroot/logo.ico",
                Width = 1020,
                Height = 640,
                Center = true,
                Show = false,
                AutoHideMenuBar = true
                //WebPreferences = new WebPreferences
                //{
                //    NodeIntegration = false,
                //}
            }
            //,$"http://localhost:{ BridgeSettings.WebPort }"
            );

            //���ص�����,���ڹر�ʱ��С��������
            Electron.IpcMain.On("hideToSystemTray", (e) =>
            {
                var codeSettings = new ConfigHelper().Get<CodeSettings>("codesettings", AppContext.BaseDirectory);
                if (codeSettings.CloseToTray)
                {
                    mainWindow.Hide();
                    if (Electron.Tray.MenuItems.Count == 0)
                    { 
                        //�������Ҽ��˵�
                        Electron.Tray.Show(AppContext.BaseDirectory + "/wwwroot/logo.ico", new MenuItem[]
                        {
                            new MenuItem
                            {
                                Label = "��SuperCode",
                                Click = () => mainWindow.Show()
                            },
                            new MenuItem
                            {
                                Label = "�˳�",
                                Icon = AppContext.BaseDirectory + "/assets/images/tray/quit.png",
                                Click = () => Electron.App.Exit()
                            }
                        });
                        Electron.Tray.SetToolTip("SuperCode");
                        //������ͼ�굥��
                        Electron.Tray.OnClick += (TrayClickEventArgs args, Rectangle rectangle) =>
                        {
                            mainWindow.Show();
                        };
                    }
                }
                else
                {
                    Electron.App.Exit();
                }
            });

            //�����Զ�����
            var codeSettings = new ConfigHelper().Get<CodeSettings>("codesettings", AppContext.BaseDirectory);
            Electron.App.SetLoginItemSettings(new LoginSettings { OpenAtLogin = codeSettings.OpenAtLogin });

            //�����ߵ��Թ���
            //if (env.IsDevelopment())
            //{
            //    browserWindow.WebContents.OpenDevTools();
            //}

            //���������ʾ
            //await mainWindow.WebContents.Session.ClearCacheAsync();
            //mainWindow.Maximize();

            mainWindow.OnReadyToShow += () => mainWindow.Show();
        }
    }
}
