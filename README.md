# Movie Theater Application

## Description
A website to create movie tickets. User selects movie, showing, and seat. After that a ticket will be created and saved in the database. Its nothing amazing but I just wanted to think about how seat selection worked (in database and in display) in these types of apps.

## Tools
This project uses C#, ASP.NET Core MVC and Microsoft Sql Server.

## Running
- Install .net10 SDK/runtime and Sql Server
- Create .env file (There is an Example_Env.txt file in the App folder) with DB Connection String
- Use migration file to create tables in Db
	- dotnet ef database update
- Run
	- dotnet run