using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RouletteBets.Api.Models;
using RouletteBets.Api.Repositories;

namespace RouletteBets.Api.Controllers
{
    [ApiController]
    [Route("api/roulettes")]
    public class RouletteController : ControllerBase
    {
        private readonly IRouletteRepository _repository;
        public RouletteController(IRouletteRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public async Task<ActionResult<string>> Create(Roulette roulette)
        {
            if (!ModelState.IsValid) return BadRequest();
            roulette.state = RouletteStatesEnum.OPEN;

            return await _repository.Add(roulette);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> Open(string id)
        {
            try{
                await _repository.UpdateState(id, RouletteStatesEnum.OPEN);

                return NoContent();
            } catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roulette>>> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}