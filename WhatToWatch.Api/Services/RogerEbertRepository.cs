using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatToWatch.Api.ExternalModels;
using WhatToWatch.Api.Library;

namespace WhatToWatch.Api.Services
{
    public class RogerEbertRepository : IRogerEbertRepository
    {
        public RogerEbertMovieRating GetMovieRating(string movieName)
        {
            if (String.IsNullOrWhiteSpace(movieName))
                throw new ArgumentNullException(nameof(movieName));

            var rogertEbertMovieRating = new RogerEbertMovieRating();

            using (var browser = new ChromeDriver(WebScraper.ChromeOptions))
            {
                var movieNameQuery = movieName.Trim().Replace(' ', '+');
                var searchPageHtmlDocumentNode = WebScraper.GetHtmlDocumentNodeFromUrl(browser, $"https://www.rogerebert.com/search?utf8=%E2%9C%93&q={movieNameQuery}");

                rogertEbertMovieRating.ReviewTitle = searchPageHtmlDocumentNode.SelectSingleNode("//*[@class='gs-title'][1]")?.FirstChild.InnerText;
                rogertEbertMovieRating.ReviewLink = searchPageHtmlDocumentNode.SelectSingleNode("//*[@class='gs-title'][1]")?.FirstChild.Attributes["href"].Value;

                if (String.IsNullOrWhiteSpace(rogertEbertMovieRating.ReviewLink))
                    return null;

                var reviewPageHtmlDocumentNode = WebScraper.GetHtmlDocumentNodeFromUrl(browser, rogertEbertMovieRating.ReviewLink);

                var starRating = reviewPageHtmlDocumentNode.SelectSingleNode("//*[@class='star-rating']").ChildNodes;
                if (starRating.First().Attributes["title"].Value == "thumbsdown")
                {
                    rogertEbertMovieRating.Rating = "thumbsdown :(";
                }
                else
                {
                    var ratingInNumber = starRating.Sum(star =>
                    {
                        if (star.Attributes["title"].Value == "star-full")
                            return 1; // full star
                        else
                            return 0.5; // half star
                    });
                    rogertEbertMovieRating.Rating = ratingInNumber + " / 4";
                }
            }

            return rogertEbertMovieRating;
        }
    }
}
