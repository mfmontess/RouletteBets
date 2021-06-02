using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Roulette.Api.Models;

namespace Roulette.Api.Repositories
{
    public class BetRepository : IBetRepository
    {
        private MongoDBContext db;
        public BetRepository(IConnection connection){
            db = new MongoDBContext(connection);
        }
        public async Task<Bet> Add(Bet bet)
        {
            await db.Bets.InsertOneAsync(bet);
            return bet;
        }

        public async Task<List<Bet>> GetByRouletteId(string rouletteId)
        {
            return await db.Bets.Find(x => x.rouletteId == rouletteId).ToListAsync();
        }

        public async void Update(Bet bet)
        {
            await db.Bets.ReplaceOneAsync(x => x.id == bet.id ,bet);
        }
    }
}