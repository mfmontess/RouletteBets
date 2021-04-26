using MongoDB.Driver;
using Roulette.Api.Models;

namespace Roulette.Api.Repositories
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _mongodb;
        public MongoDBContext()
        {
            var client = new MongoClient("mongodb://localhost:27017/");
            _mongodb = client.GetDatabase("RouletteBetsDB");
        }
        public IMongoCollection<Roulette.Api.Models.Roulette> Roulettes
        {
            get{
                return _mongodb.GetCollection<Roulette.Api.Models.Roulette>("Roulettes");
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