using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
namespace RouletteBets.Api.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RouletteStatesEnum
    {
        [BsonElement("OPEN"), EnumMember(Value = "OPEN")]
        OPEN,
        [BsonElement("CLOSED"), EnumMember(Value = "CLOSED")]
        CLOSED
    }
}