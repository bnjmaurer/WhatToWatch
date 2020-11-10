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
            
            var reviewInfo = Regex.Split(firstResultHtmlNode.InnerText, @"[[][]][-]");
            
            return kidsInMindMovieRating;
        }
    }
}
