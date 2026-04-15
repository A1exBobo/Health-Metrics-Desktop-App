using System;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

public static class DatabaseInitializer
{
    public static void Initialize()
    {
        string dbFile = Path.Combine(
            AppDomain.CurrentDomain.GetData("DataDirectory").ToString(),
            "MyDatabase.mdf");

        if (!File.Exists(dbFile))
        {
            CreateDatabase(dbFile);
        }

        RunSqlScript();
    }

    private static void CreateDatabase(string dbPath)
    {
        string masterConnection =
            @"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True;";

        using (var connection = new SqlConnection(masterConnection))
        {
            connection.Open();

            string cmdText = $@"
                CREATE DATABASE MyDatabase
                ON (NAME = N'MyDatabase', FILENAME = '{dbPath}')";

            using (var cmd = new SqlCommand(cmdText, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

    private static void RunSqlScript()
    {
        string connectionString =
            ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString;

        string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Queries","init.sql");

        string script = File.ReadAllText(scriptPath);

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (var cmd = new SqlCommand(script, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}