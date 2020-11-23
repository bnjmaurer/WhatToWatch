using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatToWatch.Api.ExternalModels
{
    public class Result
    {
        public string release_date { get; set; }
        public int id { get; set; }
        public string title { get; set; }
    }
    public class MovieDbApiSearchMovie
    {
        public List<Result> results { get; set; }
    }
}
