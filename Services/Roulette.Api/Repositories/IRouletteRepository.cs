using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roulette.Api.Repositories
{
    public interface IRouletteRepository
    {
        Task<string> Add(Roulette.Api.Models.Roulette roulette);
        Task UpdateState(string id, string state);
        Task<Roulette.Api.Models.Roulette> Get(string id);
        Task<List<Roulette.Api.Models.Roulette>> GetAll();
    }
}