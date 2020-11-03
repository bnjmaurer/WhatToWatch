using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatToWatch.Api.ExternalModels;

namespace WhatToWatch.Api.Models
{
    public class MovieInfo
    {
        public string MovieName { get; set; }
        public RogerEbertMovieRating RogerEbert { get; set; }
    }
}
