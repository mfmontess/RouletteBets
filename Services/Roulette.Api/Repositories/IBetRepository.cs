using System.Collections.Generic;
using System.Threading.Tasks;
using RouletteBets.Api.Models;
namespace RouletteBets.Api.Repositories
{
    public interface IBetRepository
    {
        Task<Bet> Add(Bet bet);
        Task<List<Bet>> GetByRouletteId(string rouletteId);
        void Update(Bet bet);
    }
}