using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatToWatch.Api.ExternalModels;

namespace WhatToWatch.Api.Services
{
    public interface IKidsInMindRepository
    {
        public KidsInMindMovieRating GetMovieRating(string movieName);
    }
}
