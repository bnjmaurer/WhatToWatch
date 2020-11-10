using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatToWatch.Api.Library
{
    public static class HtmlAgilityPackWebScraper
    {
        public static HtmlNode GetHmtlDocumentNodeFromUrl(string url)
        {            
            var webPage = new HtmlWeb();
            return webPage.Load(url).DocumentNode;
        }
    }
}
