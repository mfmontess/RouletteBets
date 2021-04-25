using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Roulette.Api.Models
{
    public class Roulette
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string name { get; set; }
        public string state { get; set; }
    }
}