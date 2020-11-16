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
        private readonly IKidsInMindRepository kidsInMindRepository;

        public MovieInfoController(IRogerEbertRepository rogerEbertRepository, IKidsInMindRepository kidsInMindRepository)
        {
            this.rogerEbertRepository = rogerEbertRepository;
            this.kidsInMindRepository = kidsInMindRepository;
        }

        [HttpGet("{movieName}")]
        public ActionResult<MovieInfo> Get(string movieName)
        {            
            var movieInfo = new MovieInfo();
            //movieInfo.RogerEbert = rogerEbertRepository.GetMovieRating(movieName);
            movieInfo.KidsInMind = kidsInMindRepository.GetMovieRating(movieName);
            return Ok(movieInfo);           
        }
    }
}
