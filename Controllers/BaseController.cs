using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LeagueManager.Models;
using System.Web;
using System.Web.Mvc;

namespace LeagueManager.Controllers
{
    public class BaseController : Controller
    {

        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }

        
    }
}
