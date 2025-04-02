using Dapper;
using Microsoft.Data.Sqlite;


namespace AMS_News.Infraestructure.Data.Database;

public static class CreateDatabase
{
    public static void Create(string connectionString)
    {
        SQLitePCL.Batteries.Init();
        var connectionStringBuilder = new SqliteConnectionStringBuilder(connectionString);
        var databaseName = connectionStringBuilder.DataSource;
        using var connector = new SqliteConnection(connectionString);
        var param = new DynamicParameters();
        param.Add("databaseName", databaseName);
        if (!File.Exists(databaseName))
            using (File.Create(databaseName)) { }
    }
}