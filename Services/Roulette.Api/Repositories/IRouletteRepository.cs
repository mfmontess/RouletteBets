using RouletteBets.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace RouletteBets.Api.Repositories
{
    public interface IRouletteRepository
    {
        Task<string> Add(Roulette roulette);
        Task UpdateState(string id, string state);
        Task<Roulette> Get(string id);
        Task<List<Roulette>> GetAll();
    }
}