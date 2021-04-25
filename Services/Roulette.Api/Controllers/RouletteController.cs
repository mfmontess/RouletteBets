using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Roulette.Api.Controllers
{
    [ApiController]
    [Route("roulettes")]
    public class RouletteController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Roulette.Api.Models.Roulette>> GetAll()
        {
            return new List<Roulette.Api.Models.Roulette>();
        }
    }
}