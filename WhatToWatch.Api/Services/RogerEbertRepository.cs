using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
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
             
            //using (var browser = new ChromeDriver(WebScraper.chromeDriverService, WebScraper.chromeOptions, TimeSpan.FromSeconds(30)))
            using (var browser = new ChromeDriver(SeleniumChromeDriverWebScraper.chromeDriverService, SeleniumChromeDriverWebScraper.chromeOptions))
            {
                //browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);                
                string searchPageLink = "https://www.rogerebert.com/search?utf8=%E2%9C%93&q=" + movieName.Trim().Replace(' ', '+');
                var searchPageHtml = SeleniumChromeDriverWebScraper.GetHtmlDocumentNodeFromUrl(browser, searchPageLink);

                rogertEbertMovieRating.ReviewLink = searchPageHtml.SelectSingleNode("//*[@class='gs-title'][1]")?.FirstChild.Attributes["href"].Value;

                if (String.IsNullOrWhiteSpace(rogertEbertMovieRating.ReviewLink))
                    return null;

                rogertEbertMovieRating.ReviewTitle = searchPageHtml.SelectSingleNode("//*[@class='gs-title'][1]")?.FirstChild.InnerText;

                var reviewPageHtml = SeleniumChromeDriverWebScraper.GetHtmlDocumentNodeFromUrl(browser, rogertEbertMovieRating.ReviewLink);

                var starRating = reviewPageHtml.SelectSingleNode("//*[@class='star-rating']").ChildNodes;
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
