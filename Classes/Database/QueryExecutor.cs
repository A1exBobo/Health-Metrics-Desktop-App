using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;

public class QueryExecutor
{
    private readonly string _connectionString;

    public QueryExecutor()
    {
        _connectionString = ConfigurationManager
            .ConnectionStrings["MyDb"].ConnectionString;
    }


    public DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
    {
        var table = new DataTable();

        using (var conn = new SqlConnection(_connectionString))
        {
            conn.Open();

            using (var cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                using (var adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
        }

        return table;
    }

    public static class SqlFiles
    {
        public const string InsertPerson = @"Queries\Buttons\AddPersFormButtonQueries\insertNewPerson.sql";
        public const string DeletePerson = @"Queries\Buttons\AddPersFormButtonQueries\DeletePersonQuery.sql";
        public const string SelectPerson = @"Queries\Buttons\AddPersFormButtonQueries\SelectPersAndPersID.sql";
        public const string InsertMeasurements = @"Queries\Buttons\AddPersFormButtonQueries\InsertMeasurement.sql";
        //etc...

        /*
         usage 
        ExecuteQueryFromResource(SqlFiles.GetUsers);
         */

    }

    private string GetSql(string fileName)
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

        if (!File.Exists(path))
            throw new Exception($"SQL file not found: {path}");

        return File.ReadAllText(path);
    }

    public DataTable ExecuteQueryFromResource(string fileName, params SqlParameter[] parameters)
    {
        string query = GetSql(fileName);
        return ExecuteQuery(query, parameters);
    }

    public int ExecuteNonQuery(string query, params SqlParameter[] parameters)
    {
        using (var conn = new SqlConnection(_connectionString))
        {
            conn.Open();

            using (var cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteNonQuery();
            }
        }
    }

    public int ExecuteNonQueryFromResource(string fileName, params SqlParameter[] parameters)
    {
        string query = GetSql(fileName);
        return ExecuteNonQuery(query, parameters);
    }

    public object ExecuteScalar(string query)
    {
        using (var conn = new SqlConnection(_connectionString))
        {
            conn.Open();

            using (var cmd = new SqlCommand(query, conn))
            {
                return cmd.ExecuteScalar();
            }
        }
    }
}



/*
 How to use 


var executor = new QueryExecutor();

var result = executor.ExecuteFromFile(
    "GetPersonsByBMI.sql",
    new SqlParameter("@BMI", 25));
 
HOW IT WORKS 
SELECT * FROM Persons WHERE BMI > @BMI;



MULTIPLE Parmeters 
var table = executor.ExecuteQuery(
    "SELECT * FROM Persons WHERE BMI > @BMI AND Age < @Age",
    new SqlParameter("@BMI", 25),
    new SqlParameter("@Age", 40)
);
 */