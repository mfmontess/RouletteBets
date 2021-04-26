using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roulette.Api.Models;
using Roulette.Api.Repositories;

namespace Roulette.Api.Controllers
{
    [ApiController]
    [Route("api/bets")]
    public class BetController : ControllerBase
    {
        private readonly IBetRepository _repository;
        public BetController(IBetRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public async Task<ActionResult<Bet>> Create(Bet bet)
        {
            if(!isAuthorized())
                return Unauthorized();
            if (!ModelState.IsValid || !bet.IsValid || !IsRouletteAvailable(bet))
                return BadRequest();
            bet.state = "PLAYING";
            bet.userId = Request.Headers["Authorization"];

            return await _repository.Add(bet);
        }
        [HttpPut]
        public async Task<ActionResult<IEnumerable<Bet>>> Close([FromQuery] string rouletteId)
        {
            var bets = await _repository.GetByRouletteId(rouletteId);
            await new RouletteRepository().UpdateState(rouletteId, "CLOSED");
            CalculateWinners(bets);
            bets.ForEach(x => _repository.Update(x));
            return bets;
        }
        private void CalculateWinners(List<Bet> bets)
        {
            int numberWinner = new Random().Next(1,36);
            string colorWinner = CalculateColorWinner(numberWinner);
            foreach(Bet bet in bets){
                if(bet.number == numberWinner){
                    bet.state = "WINNER";
                    bet.earnedValue = bet.value * 5;
                } else if(string.Equals(colorWinner,bet.color)){
                    bet.state = "WINNER";
                    bet.earnedValue = bet.value * 1.8m;
                } else
                    bet.state = "PLAYED";
            }
        }
        private string CalculateColorWinner(int number)
        {
            if(number%2==0)
                return "rojo";
            else
                return "negro";
        }
        private bool IsRouletteAvailable(Bet bet)
        {
            return string.Equals(bet.roulette.state,"OPEN");
        }
        private bool isAuthorized(){
            string headerValue = string.Empty;
            if (Request.Headers.ContainsKey("Authorization"))
                headerValue = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(headerValue))
                return false;
            else
                return true;
        }
    }
}