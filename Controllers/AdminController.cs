using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebMatrix.WebData;
using LeagueManager.Filters;
using LeagueManager.Models;

namespace LeagueManager.Controllers
{
    [Authorize(Roles="Admin")]

    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        private ILeagueManagerRepository repo;

        public AdminController(ILeagueManagerRepository ILR)
        {
            repo = ILR;
        }
        public AdminController():this(new LeagueManagerRepository())
        { 
        
        }

        public ActionResult Dashboard()
        {
            var res = repo.GetUsersList();
            return View(res);
        }

        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult DeleteUser(int id)
        {
            string str= repo.DeleteUser(id);
            if(str=="Success")
            {
                ModelState.Clear();
                TempData["msg"] = "User deleted successfully from the system";
                return RedirectToAction("Dashboard");
            }
            return View();
        }


        public ActionResult Edit(int UserID)
        {
            var res = repo.GetUserDetailsByID(UserID);
            if (Request.IsAjaxRequest())
            {
                
                return View("_EditUser", res);
            }
            return View("_EditUser",res);
        }


        [HttpPost]
        public ActionResult UpdateUser(UserProfile U, string Command)
        {
            if(Command=="Edit")
            {
                string str= repo.EditUser(U);
                TempData["msg"] = "User is updated successfully.";
                return RedirectToAction("Dashboard");
            }
            return View();
        }

    }
}
