# CloudWatch_LogStream_getting_test
AWSのCloudWatchのLog StreamNameをすべて取得するのに苦労したので作成  

# Usage  
App.configにキーとかを設定  
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appsettings>
    <add key="accessKey" value=""/>
    <add key="secretAccessKey" value=""/>
    <add key="groupName" value=""/>
  </appsettings>
</configuration>
```
実行   
```bash
$ dotnet CloudWatch_LogStream_getting_test.dll
```
