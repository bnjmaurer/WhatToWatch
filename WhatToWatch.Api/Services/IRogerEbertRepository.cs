using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatToWatch.Api.ExternalModels;

namespace WhatToWatch.Api.Services
{
    public interface IRogerEbertRepository
    {
        public RogerEbertMovieRating GetMovieRating(string movieName);
    }
}
