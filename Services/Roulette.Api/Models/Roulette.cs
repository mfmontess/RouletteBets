using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
namespace RouletteBets.Api.Models
{
    public class Roulette
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [Required (ErrorMessage = "el campo 'name' es requerido")]
        public string name { get; set; }
        [JsonIgnore]
        public string state { get; set; }
    }
}