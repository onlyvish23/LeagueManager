using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeagueManager.Models;

namespace LeagueManager.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        //public ActionResult Index()
        //{
        //    return View();
        //}


        protected virtual new CustomPrincipal User
        {
            get{
                return HttpContext.User as CustomPrincipal; 
            }
         }
    }
}
