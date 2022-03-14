global using PajdisekGitMirrorer.CommonLib;
using RabbitMQ.Client;
using System.Text;
using PajdisekGitMirrorer.MirroringScaledWorker;

Settings.GetSettingsENV();

var factory = new ConnectionFactory() { HostName = Settings.RABBITMQ_HOSTNAME, UserName = Settings.RABBITMQ_USERNAME, Password = Settings.RABBITMQ_PASSWORD, VirtualHost = "/", };
using (var connection = factory.CreateConnection())
{
    using (var channel = connection.CreateModel())
    {
        try
        {
            var x = Encoding.UTF8.GetString(channel.BasicGet(Settings.RABBITMQ_QUEUE, true).Body.ToArray());
            Console.WriteLine(x);
            var message = System.Text.Json.JsonSerializer.Deserialize<RepoInfo>(x);
            Console.WriteLine(message.SourceRepository);
            var objectclone = PajdisekGitMirrorer.SadGitLibrary.git.cloneallbranches(message.SourceRepository, message.Name);
            var objectpush = PajdisekGitMirrorer.SadGitLibrary.git.pushallbranches(message.TargetRepository, message.Name);

            pushreport();
        }
        catch (System.NullReferenceException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}


static void pushreport()
{

}

