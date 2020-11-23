using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatToWatch.Api.Models;

namespace WhatToWatch.Api.Services
{
    public interface IMovieInfoRepository
    {
        public Task<MovieInfo> FindMovieAsync(string movieName);
        public Task GetMovieDetailsAsync(MovieInfo movieInfo);
    }
}
