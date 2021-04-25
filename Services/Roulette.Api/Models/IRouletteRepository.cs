using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roulette.Api.Models
{
    public interface IRouletteRepository
    {
        Task<string> Add(Roulette roulette);
        Task UpdateState(string id, string state);
        Task<Roulette> Get(string id);
        Task<List<Roulette>> GetAll();
    }
}