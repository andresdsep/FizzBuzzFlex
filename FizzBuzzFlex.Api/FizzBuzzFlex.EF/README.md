# FizzBuzzFlex DB

Database has been built with EF code first migrations.

Add a migration with:

```
dotnet ef migrations add <MigrationName> -s ../FizzBuzzFlex.Api
```

Run outstanding migrations against the database with:

```
dotnet ef database update -s ../FizzBuzzFlex.Api
```
