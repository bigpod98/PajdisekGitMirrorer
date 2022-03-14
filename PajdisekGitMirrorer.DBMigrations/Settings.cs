global using static System.Environment;

namespace PajdisekGitMirrorer.DBMigrations;
public static class Settings
{
    public static string PSQL_HOSTNAME { get; set; }
    public static string PSQL_USERNAME { get; set; }
    public static string PSQL_PASSWORD { get; set; }
    public static string PSQL_DATABASE { get; set; }


    public static void GetSettingsENV()
    {
        PSQL_HOSTNAME = GetEnvironmentVariable("PSQL_HOSTNAME");
        PSQL_USERNAME = GetEnvironmentVariable("PSQL_USERNAME");
        PSQL_PASSWORD = GetEnvironmentVariable("PSQL_PASSWORD");
        PSQL_DATABASE = GetEnvironmentVariable("PSQL_DATABASE");
    }
}