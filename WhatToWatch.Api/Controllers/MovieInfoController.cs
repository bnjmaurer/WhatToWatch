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
        private readonly IMovieInfoRepository movieInfoRepository;
        private readonly IRogerEbertRepository rogerEbertRepository;
        private readonly IKidsInMindRepository kidsInMindRepository;

        public MovieInfoController(IMovieInfoRepository movieInfoRepository, IRogerEbertRepository rogerEbertRepository, IKidsInMindRepository kidsInMindRepository)
        {
            this.movieInfoRepository = movieInfoRepository;
            this.rogerEbertRepository = rogerEbertRepository;
            this.kidsInMindRepository = kidsInMindRepository;
        }

        [HttpGet("{movieName}")]
        public async Task<ActionResult<MovieInfo>> Get(string movieName)
        {
            var movieInfo = await movieInfoRepository.FindMovieAsync(movieName);
            if (movieInfo is null)
                return NotFound();
            await movieInfoRepository.GetMovieDetailsAsync(movieInfo);
            movieInfo.RogerEbert = rogerEbertRepository.GetMovieRating(movieInfo.Name);
            movieInfo.KidsInMind = kidsInMindRepository.GetMovieRating(movieInfo.Name);
            return Ok(movieInfo);           
        }
    }
}
