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
            kidsInMindMovieRating.MovieName = movieInfo.Split('[')[0];
            kidsInMindMovieRating.MovieYear = Regex.Match(movieInfo, @"\[\d{4}\]").Value.Substring(0, 5);

            var ratings = Regex.Match(movieInfo, @"\d\.\d\.\d").Value.Split('.');
            int sexNudity;
            Int32.TryParse(ratings[0], out sexNudity);
            kidsInMindMovieRating.SexNudity = sexNudity;
            int violenceGore;
            Int32.TryParse(ratings[1], out violenceGore);
            kidsInMindMovieRating.ViolenceGore = violenceGore;
            int language;
            Int32.TryParse(ratings[2], out language);
            kidsInMindMovieRating.Language = language;

            return kidsInMindMovieRating;
        }
    }
}
