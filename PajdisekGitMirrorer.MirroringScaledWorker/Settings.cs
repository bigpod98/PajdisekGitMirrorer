global using static System.Environment;

namespace PajdisekGitMirrorer.MirroringScaledWorker;

public static class Settings
{
    public static string RABBITMQ_HOSTNAME { get; set; }
    public static string RABBITMQ_USERNAME { get; set; }
    public static string RABBITMQ_PASSWORD { get; set; }
    public static string RABBITMQ_QUEUE { get; set; }
    public static string RABBITMQ_EXCHANGE { get; set; }

    public static void GetSettingsENV()
    {
        RABBITMQ_HOSTNAME = GetEnvironmentVariable("RABBITMQ_HOSTNAME");
        RABBITMQ_USERNAME = GetEnvironmentVariable("RABBITMQ_USERNAME");
        RABBITMQ_PASSWORD = GetEnvironmentVariable("RABBITMQ_PASSWORD");
        RABBITMQ_QUEUE = GetEnvironmentVariable("RABBITMQ_QUEUE");
        RABBITMQ_EXCHANGE = GetEnvironmentVariable("RABBITMQ_EXCHANGE");
    }
}
