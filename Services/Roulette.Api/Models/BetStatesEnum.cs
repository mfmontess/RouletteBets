using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
namespace RouletteBets.Api.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BetStatesEnum
    {
        [BsonElement("PLAYING"), EnumMember(Value = "PLAYING")]
        PLAYING,
        [BsonElement("WINNER"), EnumMember(Value = "WINNER")]
        WINNER,
        [BsonElement("PLAYED"), EnumMember(Value = "PLAYED")]
        PLAYED
    }
}