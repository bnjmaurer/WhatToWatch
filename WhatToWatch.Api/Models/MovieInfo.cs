using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatToWatch.Api.ExternalModels;

namespace WhatToWatch.Api.Models
{
    public class MovieInfo
    {
        public int MovieDbApiId { get; set; }
        public string IMDBId { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int DurationInMinutes { get; set; }
        public RogerEbertMovieRating RogerEbert { get; set; }
        public KidsInMindMovieRating KidsInMind { get; set; }
    }
}
