-- Users
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' AND xtype='U')
BEGIN
    CREATE TABLE Users (
        Id INT PRIMARY KEY IDENTITY,
        Name NVARCHAR(100) NOT NULL
    );
END


-- Persons
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Persons' AND xtype='U')
BEGIN
    CREATE TABLE Persons (
        Id INT PRIMARY KEY IDENTITY,
        FirstName NVARCHAR(100) NOT NULL,
        LastName NVARCHAR(100) NOT NULL,
        FatherInitials NVARCHAR(10),
        Age INT NOT NULL
    );
END


-- Measurements (BMI, PI history)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PersonMeasurements' AND xtype='U')
BEGIN
    CREATE TABLE PersonMeasurements (
        Id INT PRIMARY KEY IDENTITY,
        PersonId INT NOT NULL,
        BMI FLOAT NOT NULL,
        PI FLOAT NOT NULL,
        CalculationDate DATETIME NOT NULL DEFAULT GETDATE(),

        FOREIGN KEY (PersonId) REFERENCES Persons(Id)
    );
END


-- Dishes
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Dishes' AND xtype='U')
BEGIN
    CREATE TABLE Dishes (
        Id INT PRIMARY KEY IDENTITY,
        Name NVARCHAR(100) NOT NULL,
        ImagePath NVARCHAR(255) NULL

    );
END


-- Nutriments (future calories etc.)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PersonNutrition' AND xtype='U')
BEGIN
    CREATE TABLE PersonNutrition (
        Id INT PRIMARY KEY IDENTITY,
        PersonId INT NOT NULL,

        Calories FLOAT,
        Protein FLOAT,
        Carbs FLOAT,
        Fat FLOAT,

        Date DATETIME NOT NULL DEFAULT GETDATE(),

        FOREIGN KEY (PersonId) REFERENCES Persons(Id)
    );
END