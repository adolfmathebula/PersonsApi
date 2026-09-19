# Persons API

A REST API built with **C#**, **ASP.NET Core**, **Entity Framework Core**, and **SQL Server**.

## Tech Stack

* C#
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server

## Database Setup

The database setup script is located at:

```text
Scripts/DatabaseSetup.sql
```

It creates the `Persons` database, tables, relationships, view, and sample data.

## Run the Project

Update the SQL Server connection string in `appsettings.json`, then run:

```bash
dotnet restore
dotnet run
```

Swagger is available when the API is running for testing the endpoints.

## Project Structure

```text
Controllers/   API controllers
Data/          Entity Framework database context
Models/        Database entities
Scripts/       SQL database setup
```
