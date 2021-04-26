using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roulette.Api.Models
{
    public interface IBetRepository
    {
        Task<Bet> Add(Bet bet);
    }
}