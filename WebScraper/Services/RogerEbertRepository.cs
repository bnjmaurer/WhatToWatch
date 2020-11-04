using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScraper.Services
{
    public class RogerEbertRepository
    {
        public static HtmlNode GetHtmlDocumentNodeFromUrl(ChromeDriver browser, string url)
        {
            var navigation = browser.Navigate();
            navigation.GoToUrl(url);

            var pageHtmlDocument = new HtmlDocument();
            pageHtmlDocument.LoadHtml(browser.PageSource);
            return pageHtmlDocument.DocumentNode;
        }

        public string GetMovieRating()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            chromeOptions.BinaryLocation = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\chrome-win\\chrome.exe";
            string result = "";
            using (var browser = new ChromeDriver(chromeOptions))
            {
                var searchPageHtmlDocumentNode = GetHtmlDocumentNodeFromUrl(browser, $"https://www.rogerebert.com/search?utf8=%E2%9C%93&q=tenet");

                result += searchPageHtmlDocumentNode.SelectSingleNode("//*[@class='gs-title'][1]")?.FirstChild.InnerText;
                var ReviewLink = searchPageHtmlDocumentNode.SelectSingleNode("//*[@class='gs-title'][1]")?.FirstChild.Attributes["href"].Value;

                var reviewPageHtmlDocumentNode = GetHtmlDocumentNodeFromUrl(browser, ReviewLink);

                var starRating = reviewPageHtmlDocumentNode.SelectSingleNode("//*[@class='star-rating']").ChildNodes;
                
                var ratingInNumber = starRating.Sum(star =>
                {
                    if (star.Attributes["title"].Value == "star-full")
                        return 1; // full star
                    else
                        return 0.5; // half star
                });
                result += ratingInNumber + " / 4";
                
            }
            return result;
        }
    }
}