SELECT 
    PersonId,
    FirstName + ' ' + FatherInitials + ' ' + LastName AS FullName
FROM Persons
ORDER BY FirstName, LastName;