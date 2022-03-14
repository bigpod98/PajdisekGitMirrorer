using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace PajdisekGitMirrorer.API;
public static class APIMethods
{
    static NpgsqlConnectionStringBuilder constringbuilder = new NpgsqlConnectionStringBuilder()
    {
        Host = Settings.PSQL_HOSTNAME,
        Username = Settings.PSQL_USERNAME,
        Password = Settings.PSQL_PASSWORD,
        Database = Settings.PSQL_DATABASE
    };

    public static class Repositories
    {
        public static RepoInfoList Get()
        {
            RepoInfoList repoInfoList = new RepoInfoList();
            List<RepoInfo> list = new List<RepoInfo>();

            NpgsqlConnectionStringBuilder constringbuilder = new NpgsqlConnectionStringBuilder()
            {
                Host = Settings.PSQL_HOSTNAME,
                Username = Settings.PSQL_USERNAME,
                Password = Settings.PSQL_PASSWORD,
                Database = Settings.PSQL_DATABASE
            };

            var connection = new NpgsqlConnection(constringbuilder.ConnectionString);
            connection.Open();
            string commandString = $"SELECT * FROM RepositoryList";
            using (var command = new NpgsqlCommand(commandString, connection))
            {


                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new RepoInfo(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                }
            }
            connection.Close();
            repoInfoList.repoInfo = list;
            return repoInfoList;
        }
    }

    public static class Repository
    {
        public static RepoInfo Get(int ID)
        {
            var connection = new NpgsqlConnection(constringbuilder.ConnectionString);
            connection.Open();
            RepoInfo repoInfo;
            string commandString = $"SELECT * FROM RepositoryList WHERE ID = @id";
            using (var command = new NpgsqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@id", ID);
                var reader = command.ExecuteReader();
                reader.Read();
                repoInfo = new RepoInfo(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));

            }
            connection.Close();

            return repoInfo;
        }

        public static string Post(RepoInfo repoInfo)
        {
            var connection = new NpgsqlConnection(constringbuilder.ConnectionString);
            connection.Open();
            string commandString = $"INSERT INTO RepositoryList(Name, SourceRepository, TargetRepository) VALUES(@name, @src, @target);";
            using (var command = new NpgsqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@name", repoInfo.Name);
                command.Parameters.AddWithValue("@src", repoInfo.SourceRepository);
                command.Parameters.AddWithValue("@target", repoInfo.TargetRepository);
                command.ExecuteNonQuery();

            }
            connection.Close();

            return "";
        }

        public static string Put(int ID, RepoInfo repoInfo)
        {

            var connection = new NpgsqlConnection(constringbuilder.ConnectionString);
            connection.Open();
            string commandString = $"UPDATE RepositoryList SET Name = @Name, SourceRepository = @src, TargetRepository = @target WHERE ID = @id";
            using (var command = new NpgsqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@id", ID);
                command.Parameters.AddWithValue("@Name", repoInfo.Name);
                command.Parameters.AddWithValue("@src", repoInfo.SourceRepository);
                command.Parameters.AddWithValue("@target", repoInfo.TargetRepository);
                command.ExecuteNonQuery();
            }
            connection.Close();

            return "";
        }
        public static string Delete(int ID)
        {

            var connection = new NpgsqlConnection(constringbuilder.ConnectionString);
            connection.Open();
            string commandString = $"DELETE FROM RepositoryList WHERE ID = @id";
            using (var command = new NpgsqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@id", ID);
                var reader = command.ExecuteNonQuery();

            }
            connection.Close();

            return "";
        }
    }

    public static class Run
    {
        public static void Get()
        {
            RepoInfoList repoinfo = Repositories.Get();
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
        }

        public static void Get(int id)
        {
            
        }
    }

    public static class Report
    {
        public static void Post(string value)
        {
            if (System.IO.Directory.Exists("/ReportLogs"))
                System.IO.File.WriteAllText($"/ReportLogs/{DateTime.Now.ToString("s")}.rlog", value);
            else
            {
                System.IO.Directory.CreateDirectory("/ReportLogs");
                System.IO.File.WriteAllText($"/ReportLogs/{DateTime.Now.ToString("s")}.rlog",value);
            }
        }
    }
}