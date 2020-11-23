//using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatToWatch.Api.ExternalModels;
using WhatToWatch.Api.Library;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace WhatToWatch.Api.Services
{
    public class KidsInMindRepository : IKidsInMindRepository
    {
        public KidsInMindMovieRating GetMovieRating(string movieName)
        {
            if (String.IsNullOrWhiteSpace(movieName))
                throw new ArgumentNullException(nameof(movieName));

            var kidsInMindMovieRating = new KidsInMindMovieRating();

            string searchPageLink = "https://kids-in-mind.com/search-desktop.htm?fwp_keyword=" + movieName.Trim().Replace(' ', '+');
            var searchPageHtml = HtmlAgilityPackWebScraper.GetHmtlDocumentNodeFromUrl(searchPageLink);
            
            var firstResultHtmlNode = searchPageHtml.SelectSingleNode("//*[@class='fwpl-item el-2onu0j search-result'][1]")?.FirstChild;
            
            if (firstResultHtmlNode is null)
                return null;

            kidsInMindMovieRating.ReviewLink = firstResultHtmlNode.Attributes["href"].Value;

            var movieInfo = firstResultHtmlNode.InnerText;
            kidsInMindMovieRating.MovieName = movieInfo.Split('[')[0].Trim();
            kidsInMindMovieRating.MovieYear = Regex.Match(movieInfo, @"\[\d{4}\]").Value.Substring(1, 4);

            var ratings = Regex.Match(movieInfo, @"\d{1,2}\.\d{1,2}\.\d{1,2}").Value.Split('.');            
            kidsInMindMovieRating.SexNudity = ratings[0] + " / 10";            
            kidsInMindMovieRating.ViolenceGore = ratings[1] + " / 10";            
            kidsInMindMovieRating.Language = ratings[2] + " / 10";

            return kidsInMindMovieRating;
        }
    }
}
