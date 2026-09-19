
-- Create Database


IF DB_ID('Persons') IS NULL
BEGIN
    CREATE DATABASE Persons;
END
GO

USE Persons;
GO



-- Create Gender Table


IF OBJECT_ID('dbo.Gender', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Gender
    (
        GenderId INT IDENTITY(1,1) PRIMARY KEY,
        Name VARCHAR(20) NOT NULL
    );
END
GO



-- Create Person Table


IF OBJECT_ID('dbo.Person', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Person
    (
        PersonId INT IDENTITY(1,1) PRIMARY KEY,
        FirstName VARCHAR(50) NOT NULL,
        LastName VARCHAR(50) NOT NULL,
        DateOfBirth DATE NOT NULL,
        Email VARCHAR(100) NULL,
        Phone VARCHAR(20) NULL,
        GenderId INT NOT NULL,

        CONSTRAINT FK_Person_Gender
            FOREIGN KEY (GenderId)
            REFERENCES dbo.Gender(GenderId)
    );
END
GO



-- Seed Gender Data


IF NOT EXISTS (SELECT 1 FROM dbo.Gender)
BEGIN
    INSERT INTO dbo.Gender (Name)
    VALUES
        ('Male'),
        ('Female'),
        ('Other');
END
GO


-- Create AdultPersons View


CREATE OR ALTER VIEW dbo.AdultPersons
AS
SELECT
    p.PersonId,
    p.FirstName,
    p.LastName,
    p.DateOfBirth,
    p.Email,
    p.Phone,
    g.GenderId,
    g.Name AS Gender
FROM dbo.Person AS p
INNER JOIN dbo.Gender AS g
    ON p.GenderId = g.GenderId
WHERE DATEADD(YEAR, 18, p.DateOfBirth) <= CAST(GETDATE() AS DATE);
GO


-- Seed Person Data

IF NOT EXISTS (SELECT 1 FROM dbo.Person)
BEGIN
    INSERT INTO dbo.Person
    (
        FirstName,
        LastName,
        DateOfBirth,
        Email,
        Phone,
        GenderId
    )
    VALUES
    (
        'John',
        'Smith',
        '1990-05-12',
        'john@example.com',
        '0821234567',
        1
    ),
    (
        'Sarah',
        'Jones',
        '1988-11-20',
        'sarah@example.com',
        '0831234567',
        2
    ),
    (
        'Peter',
        'Adams',
        '2015-02-10',
        'peter@example.com',
        '0841234567',
        1
    ),
    (
        'Emily',
        'Brown',
        '2002-07-15',
        'emily@example.com',
        '0851234567',
        2
    );
END
GO