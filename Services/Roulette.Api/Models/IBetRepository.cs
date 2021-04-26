using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roulette.Api.Models
{
    public interface IBetRepository
    {
        Task<Bet> Add(Bet bet);
        Task<List<Bet>> GetByRouletteId(string rouletteId);
        void Update(Bet bet);
    }
}