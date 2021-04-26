using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Roulette.Api.Models
{
    public class BetRepository : IBetRepository
    {
        private MongoDBContext db = new MongoDBContext();
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