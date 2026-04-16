using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

public class QueryExecutor
{
    private readonly string _connectionString;

    public QueryExecutor()
    {
        _connectionString = ConfigurationManager
            .ConnectionStrings["MyDb"].ConnectionString;
    }

    public static class SqlFiles
    {
        public const string DeletePerson = "DeletePersonQuery.sql";
        public const string InsertPerson = "InsertNewPerson.sql";
        public const string SelectPerson = "SelectPersAndPersID.sql";
        public const string InsertMeasurements = "InsertMeasurement.sql";
       //etc...

        /*
         usage 
        ExecuteQueryFromResource(SqlFiles.GetUsers);
         */

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


    private string GetSql(string fileName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // Adjust namespace to match your project
        string resourceName = $"{assembly.GetName().Name}.Queries.{fileName}";

        using (var stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream == null)
                throw new Exception($"SQL resource not found: {resourceName}");

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
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