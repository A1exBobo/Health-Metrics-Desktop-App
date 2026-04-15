using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

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