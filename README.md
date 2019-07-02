# farmapi

dotnet core + webapi + swagger + EF core + docker-compose + postgresql

Loglar consola yaziliyor buradan toplanip gereken yere gonderilebilir. https://12factor.net/logs

Exception handling `ExceptionHandlerMiddleware` ile middleware seviyesinde yapiliyor.

Model validation action filter ile yapiliyor `ModelValidateActionFilter`

Uygulama ayarlari `environment variable` da tutuluyor. Docker harici calistirilabilmesi icin ayarlar ayrica `appsettings` de tutuluyor.

API ve db beraber docker ile calistirmak icin

```sh
docker-compose up
````

veya sadece veritabanini docker ile calistirmak icin

```sh
docker-compose up db
dotnet run
```
