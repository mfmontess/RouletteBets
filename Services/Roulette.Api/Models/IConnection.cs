namespace RouletteBets.Api.Models
{
    public interface IConnection
    {
        string MongoDBStrings { get; set; }
    }
}