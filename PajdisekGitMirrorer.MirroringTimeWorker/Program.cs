using PajdisekGitMirrorer.MirroringTimeWorker;
using PajdisekGitMirrorer.CommonLib;
using RabbitMQ.Client;
using System.Text;
using Npgsql;

Settings.GetSettingsENV();

HttpClient client = new HttpClient();

var content = await client.GetAsync("http://gitmirrorer-api/Repositories").Result.Content.ReadAsStringAsync();
Console.WriteLine(content);

RepoInfoList repoinfo = JsonSerializer.Deserialize<RepoInfoList>(content);
Console.WriteLine(repoinfo);
foreach (var i in repoinfo.repoInfo)
{
    Console.WriteLine(i.SourceRepository);
}
List<string> repostring = GetStringData(repoinfo.repoInfo);

var factory = new ConnectionFactory() { HostName = Settings.RABBITMQ_HOSTNAME, UserName = Settings.RABBITMQ_USERNAME, Password = Settings.RABBITMQ_PASSWORD };
using (var connection = factory.CreateConnection())
{
    using (var channel = connection.CreateModel())
    {
        var MessageBatch = channel.CreateBasicPublishBatch();

        foreach (var message in repostring)
        {
            var body = new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(message));
            MessageBatch.Add(exchange: Settings.RABBITMQ_EXCHANGE, routingKey: Settings.RABBITMQ_QUEUE, mandatory: true, properties: null, body: body);
        }
        MessageBatch.Publish();
    }
}

System.Threading.Thread.Sleep(10000);

static List<string> GetStringData(List<RepoInfo> repoinfo)
{
    var list = new List<string>();
    foreach (var item in repoinfo)
    {
        list.Add(JsonSerializer.Serialize(item));
    }

    return list;
}