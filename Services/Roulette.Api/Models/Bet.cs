using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace RouletteBets.Api.Models
{
    public class Bet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [Range(0, 36, ErrorMessage = "Los números válidos para apostar son del 0 al 36")]
        public int number { get; set; }
        public string color { get; set; }
        [Required (ErrorMessage = "El campo 'value' es requerido")]
        [Range(1, 10000, ErrorMessage = "El rango de las apuestas se encuentra entre 1 y 10.000 dólares")]
        public decimal value { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [Required (ErrorMessage = "El campo 'rouletteId' es requerido")]
        public string rouletteId { get; set; }
        public decimal earnedValue { get; set; }
        [JsonIgnore]
        public string userId { get; set; }
        [BsonRepresentation(BsonType.String)]
        public BetStatesEnum state { get; set; }
        [JsonIgnore]
        public bool IsValid { 
            get{
                if (number > 0)
                {
                    color = string.Empty;
                    return true;
                }
                else if (IsColorValid(color))
                {
                    number = 0;
                    return true;
                }
                else return false;
            }
        }
        private static bool IsColorValid(string color){
            switch(color){
                case "rojo":
                case "negro":
                case "red":
                case "black":
                    return true;
                default:
                    return false;
            }
        }
    }
}