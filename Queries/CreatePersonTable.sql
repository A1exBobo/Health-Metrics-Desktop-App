IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Persons' AND xtype='U')
BEGIN
    CREATE TABLE Persons (
        Id INT PRIMARY KEY IDENTITY,

        FirstName NVARCHAR(100) NOT NULL,
        LastName NVARCHAR(100) NOT NULL,
        FatherInitials NVARCHAR(10),

        Age INT NOT NULL,
        BMI FLOAT NOT NULL,
        PI FLOAT NOT NULL
    );
END