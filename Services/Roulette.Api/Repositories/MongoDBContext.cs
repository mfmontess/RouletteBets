using MongoDB.Driver;
using RouletteBets.Api.Models;
namespace RouletteBets.Api.Repositories
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _mongodb;
        public MongoDBContext(IConnection _connection)
        {
            var client = new MongoClient(_connection.MongoDBStrings);
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