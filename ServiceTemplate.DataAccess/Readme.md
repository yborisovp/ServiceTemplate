# Обновление миграций
1. Найти в Solution Explorer'е проект `Host`. Выбрать в контекстом меню пункт Set as StartUp Project.
2. Открыть окно Tools -> NuGet Package Manager -> Package Manager Console.
3. В выпадающем списке `Default project`, расположенном в верхней части окна, выбрать `ServiceTemplate.DataAccess`.
4. Создать новую миграцию с помощью команды:

```
Add-Migration Initial -Context DataBaseContext
```
Или
```
dotnet ef migrations add InitialCreate --context DatabaseContext --output-dir Migrations/  --project ./src/ServiceTemplate.DataAccess --startup-project ./src/Host/ 
```

5. Создать SQL-скрипт с помощью команды:
```
Script-Migration -Context DataBaseContext -From InitialCreate -To addRouter
```
Или
```
dotnet ef migrations script InitialCreate AddCalculationType --project ./src/ServiceTemplate.DataAccess --startup-project ./src/Host/ -o ./src/ServiceTemplate.DataAccess/Migrations/SQL/ИМЯ_СКРИПТА.sql
```

6. [Опционально] Обновить базу данных командой 
```
dotnet ef database update --project ./src/ServiceTemplate.DataAccess --startup-project ./src/Host/
```

