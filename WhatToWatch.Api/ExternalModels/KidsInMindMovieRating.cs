using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatToWatch.Api.ExternalModels
{
    public class KidsInMindMovieRating
    {
        public string movieName { get; set; }
        public string movieYear { get; set; }
        public int SexNudity { get; set; }
        public int ViolenceGore { get; set; }
        public int Language { get; set; }
    }
}
