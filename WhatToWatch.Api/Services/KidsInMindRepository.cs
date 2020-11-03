using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatToWatch.Api.ExternalModels;

namespace WhatToWatch.Api.Services
{
    public class KidsInMindRepository : IKidsInMindRepository
    {
        public KidsInMindMovieRating GetMovieRating(string movieName)
        {
            if (String.IsNullOrWhiteSpace(movieName))
                throw new ArgumentNullException(nameof(movieName));

            var kidsInMindMovieRating = new KidsInMindMovieRating();

            return kidsInMindMovieRating;
        }
    }
}
