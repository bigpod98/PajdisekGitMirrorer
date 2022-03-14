using Npgsql;
using PajdisekGitMirrorer.DBMigrations;

Settings.GetSettingsENV();

NpgsqlConnectionStringBuilder constringbuilder = new NpgsqlConnectionStringBuilder()
{
    Host = Settings.PSQL_HOSTNAME,
    Username = Settings.PSQL_USERNAME,
    Password = Settings.PSQL_PASSWORD,
    Database = Settings.PSQL_DATABASE
};

Console.WriteLine(Settings.PSQL_HOSTNAME);
var connection = new NpgsqlConnection(constringbuilder.ConnectionString);
connection.Open();
string commandString = $"CREATE TABLE RepositoryList (ID serial PRIMARY KEY, Name VARCHAR(64), SourceRepository VARCHAR(256), TargetRepository VARCHAR(256));";
using (var command = new NpgsqlCommand(commandString, connection))
{
    var reader = command.ExecuteNonQuery();
}