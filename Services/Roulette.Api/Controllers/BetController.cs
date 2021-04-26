using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roulette.Api.Models;

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
            bet.state = "Playing";
            bet.userId = Request.Headers["Authorization"];

            return await _repository.Add(bet);
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