﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatToWatch.Api.ExternalModels
{
    public class KidsInMindMovieRating
    {
        public string MovieName { get; set; }
        public string MovieYear { get; set; }
        public string ReviewLink { get; set; }
        public int SexNudity { get; set; }
        public int ViolenceGore { get; set; }
        public int Language { get; set; }
    }
}
