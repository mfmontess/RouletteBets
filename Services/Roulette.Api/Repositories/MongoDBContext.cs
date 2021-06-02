using MongoDB.Driver;
using Roulette.Api.Models;

namespace Roulette.Api.Repositories
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _mongodb;
        public MongoDBContext(IConnection _connection)
        {
            var client = new MongoClient(_connection.MongoDBStrings);
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