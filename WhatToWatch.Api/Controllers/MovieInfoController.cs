using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WhatToWatch.Api.Models;
using WhatToWatch.Api.Services;

namespace WhatToWatch.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieInfoController : ControllerBase
    {
        private readonly IRogerEbertRepository rogerEbertRepository;

        public MovieInfoController(IRogerEbertRepository rogerEbertRepository)
        {
            this.rogerEbertRepository = rogerEbertRepository;
        }

        [HttpGet]
        public ActionResult<MovieInfo> Get()
        {
            string movieName = "tenet";
            var movieInfo = new MovieInfo();
            movieInfo.RogerEbert = rogerEbertRepository.GetMovieRating(movieName);
            if (movieInfo.RogerEbert is null)
                return NotFound();
            return Ok(movieInfo);           
        }
    }
}
