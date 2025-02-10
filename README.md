# FizzBuzzFlex

A full stack implementation of the FizzBuzz game, with flexible Fizz and Buzz.

## How to run

Set up a new SQL database (on your local instance) called FizzBuzzFlex. You can use this SQL script to do so:

```sql
--use master database
USE master;
GO
-- create TutorialDB database it not exist
IF NOT EXISTS ( SELECT name FROM sys.databases WHERE name = N'FizzBuzzFlex' ) CREATE DATABASE [FizzBuzzFlex];
Go
```

Then, from the root of the project, run the following scripts in two separate console windows:

```powershell
cd ./FizzBuzzFlex.Api
dotnet restore
dotnet build
cd ./FizzBuzzFlex.EF
dotnet ef database update -s ../FizzBuzzFlex.Api
cd  ../FizzBuzzFlex.Api
dotnet run
```

```powershell
cd ./FizzBuzzFlex.Web
npm i
npm run dev
```

The second console should be displaying a localhost link. Ctrl + click it and have fun with this little app!

## Wish list of functionality

- Games table with pagination

  - Currently, given a large amount of created and returned games, the list will struggle to render it all

- Number of divisors in a game to be flexible

- Enter player name before starting match

- Prompt checking to be more flexible (don't care about order, spaces)

- Prompt checking to be "optimistic". Don't make player wait for response for previous round.

- Implement form framework in all forms, including validation

- Add buttons to build prompt answer ("Fizz" button to enter Fizz on the input)

- Implement Mediatr on endpoints
