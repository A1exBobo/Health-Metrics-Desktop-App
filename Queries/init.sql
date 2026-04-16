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


-- PersonMeasurements
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PersonMeasurements' AND xtype='U')
BEGIN
    CREATE TABLE PersonMeasurements (
        Id INT PRIMARY KEY IDENTITY,
        PersonId INT NOT NULL,
        Height FLOAT NOT NULL,
        Mass FLOAT NOT NULL,
        BMI FLOAT NOT NULL,
        PI FLOAT NOT NULL,
        CalculationDate DATETIME NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY (PersonId) REFERENCES Persons(Id)
    );
END
ELSE
BEGIN
    -- Fix missing columns if table already exists
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = 'Height' AND Object_ID = Object_ID('PersonMeasurements'))
        ALTER TABLE PersonMeasurements ADD Height FLOAT;

    IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = 'Mass' AND Object_ID = Object_ID('PersonMeasurements'))
        ALTER TABLE PersonMeasurements ADD Mass FLOAT;

    IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = 'BMI' AND Object_ID = Object_ID('PersonMeasurements'))
        ALTER TABLE PersonMeasurements ADD BMI FLOAT;

    IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = 'PI' AND Object_ID = Object_ID('PersonMeasurements'))
        ALTER TABLE PersonMeasurements ADD PI FLOAT;
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


-- PersonNutrition
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


-- Seed test data (only if empty)
IF NOT EXISTS (SELECT 1 FROM Persons)
BEGIN
    INSERT INTO Persons (FirstName, LastName, FatherInitials, Age)
    VALUES 
    ('John', 'Smith', 'A', 30),
    ('Maria', 'Popescu', 'B', 25),
    ('Alex', 'Ionescu', 'C', 40);
END

IF NOT EXISTS (SELECT 1 FROM PersonMeasurements)
BEGIN
    INSERT INTO PersonMeasurements (PersonId, Height, Mass, BMI, PI)
    VALUES 
    (1, 1.80, 75, 23.15, 41.66),
    (2, 1.65, 60, 22.03, 36.36),
    (3, 1.75, 85, 27.75, 48.57);
END