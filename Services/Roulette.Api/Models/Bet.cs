using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Roulette.Api.Repositories;

namespace Roulette.Api.Models
{
    public class Bet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [Range(0, 36, ErrorMessage = "los números válidos para apostar son del 0 al 36")]
        public int number { get; set; }
        public string color { get; set; }
        [Required (ErrorMessage = "el campo 'value' es requerido")]
        [Range(0.01, 10000.00, ErrorMessage = "máximo 10.000 dólares")]
        public decimal value { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [Required (ErrorMessage = "el campo 'rouletteId' es requerido")]
        public string rouletteId { get; set; }
        public decimal earnedValue { get; set; }
        [JsonIgnore]
        public Roulette roulette{
            get {
                return new RouletteRepository().Get(rouletteId).Result;
            }
        }
        public string userId { get; set; }
        public string state { get; set; }
        [JsonIgnore]
        public bool IsValid { 
            get{
                return (IsColorValid(color) || number > 0);
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