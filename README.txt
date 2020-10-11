**GETTING STARTED**

*BACKEND*

1. Create a solution file as a container
2. dotnet new webapi -o API, outputs it to another folder (API)
3. dotnet sln add API, adds API into solution

4. File > Preferences > settings    search for exclude and add pattern for **/obj and **/bin so we dont see these files while developing

5. give permission to run the app on the browser, trust the certificate (if have not done before)
    >dotnet dev-certs https --trust

6. Start the app to auto build when changes are made
    >dotnet watch run
    -now the app should run

7. Add a folder, Entities

8. add a class of AppUser
    -add properties Id and name with getters and setters

9. bridge code and database
    -install nuget package --Microsoft.EntityFrameworkCore.Sqlite and add to API folder from the npm install options
    -create Data folder 
        -create class to manage database connection string and table properties

10. update appsettings.Development.json file to accept connection string --"ConnectionStrings" : {"DefaultConnection": "Data Source=datingapp.db"}

11. install dotnet ef tool via CLI to easily migrate data from code to database tables
     >dotnet tool install --global dotnet-ef --version 5.0.0-rc.1.20451.13

12. migration to create a database based on the code already written
    output to Data/Migrations
    dotnet ef migrations add InitialCreate -o Data/Migrations
    this will add some files inside the Data folder

13. create database
    dotnet ef database update

14. create Users controller
    inherit from controller base
    create private field + constructor
    create endpoints




*CLIENT SIDE*

1. >ng new client

2. add sercuirty certificates

-ng g -h for CLI options on what to create

>ng g c nameOfComponent --skip-test
    -creates new component
    -this way will automatically add an import statement to our app.module.ts file

-angular provides 2 way binding




********GENERAL NOTES********
-when starting back up
    >ng serve in the client folder
    > dotnet run in the api folder
    -use separate terminal windows

-to update the database Entities
    -update with new properties
    >dotnet ef migrations add NewThingAdded
    >dotnet ef database update