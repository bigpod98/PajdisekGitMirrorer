using System.Text.Json.Serialization;

namespace PajdisekGitMirrorer.CommonLib;

public class RepoInfo
{
    public RepoInfo()
    {
    }

    public RepoInfo(string iD, string name, string sourceRepository, string targetRepository)
    {
        ID = iD;
        Name = name;
        SourceRepository = sourceRepository;
        TargetRepository = targetRepository;
    }

    [JsonPropertyName("id")]
    public string ID { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("sourceRepository")]
    public string SourceRepository { get; set; }
    [JsonPropertyName("targetRepository")]
    public string TargetRepository { get; set; }

}

public class RepoInfoList
{
    public RepoInfoList()
    {
        this.repoInfo = new List<RepoInfo>(); ;
    }

    [JsonPropertyName("repoInfo")]
    public List<RepoInfo> repoInfo { get; set; }
}
