using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RouletteBets.Api.Models;
using RouletteBets.Api.Repositories;
namespace RouletteBets.Api.Controllers
{
    [ApiController]
    [Route("api/bets")]
    public class BetController : ControllerBase
    {
        private readonly IBetRepository _betRepository;
        private readonly IRouletteRepository _rouletteRepository;
        public BetController(IBetRepository betRepository, IRouletteRepository rouletteRepository)
        {
            _betRepository = betRepository;
            _rouletteRepository = rouletteRepository;
        }
        [HttpPost]
        public async Task<ActionResult<Bet>> Create([FromHeader(Name = "Authorization")] string userId, Bet bet)
        {
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            if (!ModelState.IsValid || !bet.IsValid || !IsRouletteAvailable(bet.rouletteId))
                return BadRequest();
            bet.state = BetStatesEnum.PLAYING;
            bet.userId = userId;

            return await _betRepository.Add(bet);
        }
        [HttpPut]
        public async Task<ActionResult<IEnumerable<Bet>>> Close([FromQuery] string rouletteId)
        {
            var bets = await _betRepository.GetByRouletteId(rouletteId);
            await _rouletteRepository.UpdateState(rouletteId, RouletteStatesEnum.CLOSED);
            CalculateWinners(bets);
            bets.ForEach(x => _betRepository.Update(x));

            return bets;
        }
        private void CalculateWinners(List<Bet> bets)
        {
            int numberWinner = new Random().Next(1,36);
            string colorWinner = CalculateColorWinner(numberWinner);
            foreach(Bet bet in bets){
                if(bet.number == numberWinner){
                    bet.state = BetStatesEnum.WINNER;
                    bet.earnedValue = bet.value * 5;
                } else if(string.Equals(colorWinner,bet.color)){
                    bet.state = BetStatesEnum.WINNER;
                    bet.earnedValue = bet.value * 1.8m;
                } else
                    bet.state = BetStatesEnum.PLAYED;
            }
        }
        private string CalculateColorWinner(int number)
        {
            if(number%2==0)
                return "rojo";
            else
                return "negro";
        }
        private bool IsRouletteAvailable(string rouletteId)
        {
            Roulette roulette = _rouletteRepository.Get(rouletteId).Result;

            return Equals(roulette.state,RouletteStatesEnum.OPEN);
        }
    }
}