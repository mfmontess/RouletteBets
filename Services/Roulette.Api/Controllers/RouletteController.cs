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
            roulette.state = "OPEN";

            return await _repository.Add(roulette);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> Open(string id)
        {
            string state = "OPEN";
            try{
                await _repository.UpdateState(id, state);

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