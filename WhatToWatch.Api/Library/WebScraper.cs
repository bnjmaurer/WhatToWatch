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
        public static ChromeOptions chromeOptions
        {
            get
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments("headless");
                chromeOptions.AddArguments("no-sandbox");
                chromeOptions.AddArguments("disable-setuid-sandbox");
                return chromeOptions;
            }
        }

        public static ChromeDriverService chromeDriverService
        {
            get
            {
                string driverPath = "/opt/selenium";
                string driverExecutableFileName = "chromedriver";
                var service = ChromeDriverService.CreateDefaultService(driverPath, driverExecutableFileName);
                return service;
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
