using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Roulette.Api.Models
{
    public class RouletteRepository : IRouletteRepository
    {
        private MongoDBContext db = new MongoDBContext();
        public async Task<string> Add(Roulette roulette)
        {
            await db.Roulettes.InsertOneAsync(roulette);
            return roulette.id;
        }
        public async Task<Roulette> Get(string id)
        {
            return await db.Roulettes.Find(x => x.id == id).FirstOrDefaultAsync();
        }
        public async Task<List<Roulette>> GetAll()
        {
            return await db.Roulettes.Find(x => true).ToListAsync();
        }
        public async Task UpdateState(string id, string state)
        {
            var filter = Builders<Roulette>.Filter.Eq("id", id);
            var update = Builders<Roulette>.Update.Set("state", state);
            await db.Roulettes.UpdateOneAsync(filter,update);
        }
    }
}