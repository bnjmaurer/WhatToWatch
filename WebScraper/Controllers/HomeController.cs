using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebScraper.Services;

namespace WebScraper.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            var repp = new RogerEbertRepository();

            return repp.GetMovieRating();
        }
    }
}
