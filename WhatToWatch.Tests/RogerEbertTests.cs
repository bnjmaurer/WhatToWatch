using System;
using WhatToWatch.Api.Services;
using Xunit;

namespace WhatToWatch.Tests
{
    public class RogerEbertTests
    {
        [Fact]
        public void ReturnsTheRightRating()
        {
            IRogerEbertRepository rogerEbertRepository = new RogerEbertRepository();
            var rogerEbertRating = rogerEbertRepository.GetMovieRating("Star Wars The Rise of Skywalker");
            Assert.Equal("2,5 / 4", rogerEbertRating.Rating);
        }

        [Fact]
        public void ReturnsAThumbsdownRating()
        {
            IRogerEbertRepository rogerEbertRepository = new RogerEbertRepository();
            var rogerEbertRating = rogerEbertRepository.GetMovieRating("Paul Blart Mall Cop 2");
            Assert.Equal("thumbsdown :(", rogerEbertRating.Rating);
        }

        [Fact]
        public void NoUnhandledExceptionWhenMovieNotFound()
        {
            IRogerEbertRepository rogerEbertRepository = new RogerEbertRepository();
            var rogerEbertRating = rogerEbertRepository.GetMovieRating("ThisMovieDoesNotExist");
            Assert.Null(rogerEbertRating);
        }
    }
}
