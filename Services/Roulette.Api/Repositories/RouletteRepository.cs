using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Roulette.Api.Repositories
{
    public class RouletteRepository : IRouletteRepository
    {
        private MongoDBContext db = new MongoDBContext();
        public async Task<string> Add(Roulette.Api.Models.Roulette roulette)
        {
            await db.Roulettes.InsertOneAsync(roulette);
            return roulette.id;
        }
        public async Task<Roulette.Api.Models.Roulette> Get(string id)
        {
            return await db.Roulettes.Find(x => x.id == id).FirstOrDefaultAsync();
        }
        public async Task<List<Roulette.Api.Models.Roulette>> GetAll()
        {
            return await db.Roulettes.Find(x => true).ToListAsync();
        }
        public async Task UpdateState(string id, string state)
        {
            var filter = Builders<Roulette.Api.Models.Roulette>.Filter.Eq("id", id);
            var update = Builders<Roulette.Api.Models.Roulette>.Update.Set("state", state);
            await db.Roulettes.UpdateOneAsync(filter,update);
        }
    }
}