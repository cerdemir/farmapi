# farmapi

dotnet core + webapi + swagger + EF core + docker-compose + postgresql

## Loglama

Loglar consola yaziliyor buradan toplanip gereken yere gonderilebilir. [12factor.net/logs](https://12factor.net/logs)

## Exception handling

Exception handling `ExceptionHandlerMiddleware` ile middleware seviyesinde yapiliyor.

## Model validation

Model validation action filter ile yapiliyor `ModelValidateActionFilter`

## Configuration

Uygulama ayarlari `environment variable` da tutuluyor. Docker harici calistirilabilmesi icin ayarlar ayrica `appsettings` de tutuluyor.

## Run

API ve db beraber docker ile calistirmak icin

```sh
docker-compose up
```

veya sadece veritabanini docker ile calistirmak icin

```sh
docker-compose up db
dotnet run
```
