# ����

SuperCode��֧�ֿ�ƽ̨�Ŀͻ��ˣ���Ҫ����������̨Adminǰ��˷�����롣ʹ�õ��ļ�������Net5��Blazor Server��Electron��FreeSql

## ��������

> ��������
>
>  [Visual Studio 2019](https://visualstudio.microsoft.com/zh-hans/downloads/)
> 
> SDK
>
>  [.Net 5](https://dotnet.microsoft.com/download/dotnet-core)
>  
> Nuget�� 
>
> [Electron.NET](https://github.com/ElectronNET/Electron.NET)��[FreeSql](https://github.com/2881099/FreeSql)��Blazor Server��Element
> 
> ����������
>
> Sqlite
>  

## ���ù���

* Blazor������ǩ·�ɹ��ܣ�֧��ѡ��͵����˵��໥����
* ���ݿ����ӹ���֧�����ݿ����Ӳ��Թ���
* ����ģ�幤�߹���֧��ģ�幤�ߵİ�װ��ж�ء��������ܣ�֧�ִ������Զ������ڴ����ļ�Ŀ¼
* ϵͳ����֧�ֿ����Զ����������ڹر�ʱ��С�������̣��Զ�������ļ�����·���ʹ�Ŀ¼����

## ��Դ��ַ

* github�� [https://github.com/zhontai/SuperCode](https://github.com/zhontai/SuperCode)
* Gitee�� [https://gitee.com/zhontai/SuperCode](https://gitee.com/zhontai/SuperCode)

## ��������

*********************************************************
### ��Ŀ���غ��Ȱ�װElectronNET.CLI����
```
dotnet tool install ElectronNET.CLI -g
```

### ����SuperCodeĿ¼�°�װǰ����Ҫ��������
```
npm install
��
npm install --registry=https://registry.npm.taobao.org
```

### ��ʾ
>�����װeletron��node install.js�ر�����������̨����
>npm config edit��
>��.npmrc�ļ�������
>ELECTRON_MIRROR="https://npm.taobao.org/mirrors/electron/"
>���沢�˳�

### ��װ��ɺ󣬵�������ѡ��SuperCode Watch��������
��������˵��
* SuperCode Watch ��������SuperCode�ͻ��˲������ļ��޸��Զ����룬�ٶȽϿ죬���Կͻ����Ƽ���
* SuperCode Start ��������SuperCode�ͻ��ˣ��ٶȽ�����
* SuperCode Build ������ɰ�װ�������ɫ���ļ���
* SuperCode Web �������г�Web���ģʽ������ʹ��Electron���ܣ��ٶȿ죬����Web���Ƽ���


��������ƽ̨ʱ���޸�SuperCode Build���
```
electronize build /target win
electronize build /target osx
electronize build /target linux
```

### ��ʾ
>�޸���ʽless�ļ�����Ҫ������������Դ��������ѡ������/less�Ҽ���������css�ļ�����ˢ��Web��Ŀ
