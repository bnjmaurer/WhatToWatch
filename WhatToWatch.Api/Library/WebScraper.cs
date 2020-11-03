using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatToWatch.Api.Library
{
    public static class WebScraper
    {
        public static ChromeOptions ChromeOptions
        {
            get
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments("headless");                
                return chromeOptions;
            }
        }

        public static HtmlNode GetHtmlDocumentNodeFromUrl(ChromeDriver browser, string url)
        {
            var navigation = browser.Navigate();
            navigation.GoToUrl(url);

            var pageHtmlDocument = new HtmlDocument();
            pageHtmlDocument.LoadHtml(browser.PageSource);
            return pageHtmlDocument.DocumentNode;
        }
    }
}
