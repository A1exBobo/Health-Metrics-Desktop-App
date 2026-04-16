DELETE FROM PersonMeasurements
WHERE PersonId = @PersonId;

DELETE FROM PersonNutrition
WHERE PersonId = @PersonId;

DELETE FROM Persons
WHERE Id = @PersonId;