using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeagueManager.Models;

namespace LeagueManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetRadioButtonListSelection()
        {
            ViewData["CheckCountryID"] = 2;
            CountryListing CL = new CountryListing();
            return View(CL);
            
        }

        [HttpPost]
        public string GetRadioButtonListSelection(CountryListing C)
        {
            string str = "My String";
            if (!string.IsNullOrEmpty(C.SelectedCountryID.ToString()))
            {
                str = "Selected country ID = " + C.SelectedCountryID.ToString()
                    +" and Country name: "+C.CountryList.Where(p=>p.CountryID.ToString()==C.SelectedCountryID).Select(p=>p.CountryName).ToString();
            } 
            else
            {
                str = "No country selected";
            }
            return str;
        }

    }
}
