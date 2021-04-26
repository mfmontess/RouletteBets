using MongoDB.Driver;

namespace Roulette.Api.Models
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _mongodb;
        public MongoDBContext()
        {
            var client = new MongoClient("mongodb://localhost:27017/");
            _mongodb = client.GetDatabase("RouletteBetsDB");
        }
        public IMongoCollection<Roulette> Roulettes
        {
            get{
                return _mongodb.GetCollection<Roulette>("Roulettes");
            }
        }
        public IMongoCollection<Bet> Bets
        {
            get{
                return _mongodb.GetCollection<Bet>("Bets");
            }
        }
    }
}