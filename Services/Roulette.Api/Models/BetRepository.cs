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
    }
}