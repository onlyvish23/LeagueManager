using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeagueManager.Models;
using System.Data;
namespace LeagueManager.Controllers
{
    public class LeagueController : Controller
    {
        //
        // GET: /League/

        private ILeagueManagerRepository repo;

        public LeagueController(ILeagueManagerRepository ILR)
        {
            repo = ILR;
        }

        public LeagueController(): this(new LeagueManagerRepository())
        {             
        }

        public ActionResult Index()
        {
            var res = repo.GetPlayersList();
            return View(res);
        }

        public ActionResult Create()
        {
            List<Positions> Poss = repo.GetPositions();
            ViewBag.Positions = new SelectList(Poss, "PositionID", "Position",0);

            IEnumerable<TeamsC> teams = repo.GetTeamsList();
            ViewBag.TeamList = new SelectList(teams, "TeamID", "TeamName", 0);

            List<CountryC> ctry = repo.GetCountryList();
            ViewBag.CountryList = new SelectList(ctry, "CountryID", "CountryName", 0);

            return View(new PlayersC() { CreatedDate=DateTime.Now});
        }

        [HttpPost]
        public ActionResult Create(PlayersC P)
        {
            try 
            {
                if(ModelState.IsValid)
                {
                    int i = repo.AddPlayer(P);
                    if(i>0)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch(System.Data.DataException ex)
            {
                ModelState.AddModelError("", "Error occured while inserting data " + ex.Message.ToString());
            }
            return View(P);
        }


        public ActionResult Edit(int? ID)
        {
            PlayersC P = repo.GetPlayerByID((int)ID);
            return View(P);
        }
        [HttpPost]
        public ActionResult Edit(PlayersC P)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    int id = repo.UpdatePlayer(P);
                    return RedirectToAction("Index");
                }
            }
            catch(System.Data.DataException)
            {
                ModelState.AddModelError("", "Error while editing the record");
            }
            return View(P);
        }


        public ActionResult Details(int id)
        {
            PlayersC model = repo.GetPlayerByID(id);
            return View(model);
        }
        #region Section to manage the countries

        public ActionResult CountryList()
        {
            var res = repo.GetCountryList();
            return View(res);
        }

        public ActionResult AddCountry()
        {
            return View();
        }
        

        public ActionResult InsertCountry()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertCountry(FormCollection FC)
        { 
            if(FC.HasKeys())
            {
                if (!string.IsNullOrEmpty(FC["CName"]))
                {
                    CountryC c1 = new CountryC();
                    c1.CountryName = FC["CName"].ToString();
                    int id = repo.AddCountry(c1);

                    TempData["Data"] = "Country inserted successfully for the form";
                    return RedirectToAction("CountryList");


                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddCountry(CountryC C)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    int id = repo.AddCountry(C);
                    if(id>0)
                    {
                        return RedirectToAction("CountryList");
                    }
                }
            }
            catch(DataException e)
            {
                ModelState.AddModelError("Error", "Exception:"+e.Message.ToString());
            }
            return View();
        }

        [HttpPost]
        public ActionResult DeleteCountry(CountryC C)
        {
            if (C!=null)
            {
                if (ModelState.IsValid)
                {
                    int Id= repo.DeleteCountryByID(C.CountryID);
                    if (Id > 0)
                    {
                        TempData["DeleteMessage"] = "Country deleted successfully";
                        return RedirectToAction("CountryList");
                    }
                    else {
                        TempData["DeleteMessage"] = "Cannot delete the selected country";
                    }                    
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult DeleteSelectedCountry(FormCollection FC,IEnumerable<CountryC> deleteList)
        {
            try
            {
                if (deleteList.Count() <= 0)
                {
                    TempData["ErrorMsg"] = "Please select country to delete from the list";
                    return RedirectToAction("CountryList");
                }
                
                    
                if (ModelState.IsValid)
                {
                    int ctr = 0;
                    foreach (CountryC  C in deleteList)
                    {
                        int del=repo.DeleteCountryByID(C.CountryID);
                        ctr++;
                    }

                    TempData["msg"] = "<script>alert('Change succesfully');</script>";
                    TempData["ErrorMsg"] = ctr.ToString()+" records were deleted succesfully from the system";
                    return RedirectToAction("CountryList");

                }
            }
            catch (DataException e)
            {
                ModelState.AddModelError("Error", "Exception:" + e.Message.ToString());
            }
            return View();
        }


        #endregion

        #region Section to manage the Teams
        public ActionResult TeamsList()
        {
            var res = repo.GetTeamsList();
            return View(res);
        }


        
        public ActionResult AddTeam()
        {
            List<CountryC> ctry = repo.GetCountryList();
            ViewBag.CountryList = new SelectList(ctry, "CountryID", "CountryName", 0);
            TempData["T1"] = new SelectList(ctry, "CountryID", "CountryName", 0);
            return View();
        }

        protected void LoadDDLCountry()
        {
            List<CountryC> ctry = repo.GetCountryList();
            ViewBag.CountryList = new SelectList(ctry, "CountryID", "CountryName", 0);
        }
        [HttpPost]
        public ActionResult AddTeam([Bind(Exclude="CountryName")]TeamsC objT)
        {
            try
            {
                

                if (ModelState.IsValid)
                {
                    int id = repo.AddTeam(objT);
                    if (id > 0)
                    {

                        return RedirectToAction("TeamsList");
                    }
                }
                else 
                {

                    if (objT != null)
                    {
                        ModelState.AddModelError("Error:", "No record found");
                    }

                    //LoadDDLCountry();                    
                    //var err = ModelState.Select(p => p.Value.Errors).Where(y => y.Count > 0).ToList();
                    return RedirectToAction("AddTeam");
                }
            }
            catch(DataException e)
            {
                ModelState.AddModelError("Error:", "Excpetion:"+e.Message.ToString());
            }
            return View("AddTeam");
        }

        //[HttpPost]
        

        public void LoadCountryList(int CID)
        {
            List<CountryC> ctry = repo.GetCountryList();
            ViewBag.CountryList = new CountryListForDropDown
            {
                SelectedCountryID = !string.IsNullOrEmpty(CID.ToString()) ? CID : 0,
                CountryList = ctry.Select(x => new SelectListItem
                {
                    Text = x.CountryName,
                    Value = x.CountryID.ToString(),
                    Selected= x.CountryID== CID?true:false
                })
            };
        }
        public ActionResult EditTeam(int ID)
        {
            TeamsC ot = repo.GetTeamByID(ID);
            //LoadCountryList(res.CountryID);
            List<CountryC> ctry = repo.GetCountryList();
            ot.CountryList = ctry.Select(x => new SelectListItem
                {
                    Text = x.CountryName,
                    Value = x.CountryID.ToString(),
                    Selected = x.CountryID == ot.CountryID ? true : false
                });

            
            //var res = new TeamsC
            //{
            //    CountryID = !string.IsNullOrEmpty(ot.CountryID.ToString()) ? ot.CountryID : 0,
            //    CountryList = ctry.Select(x => new SelectListItem
            //    {
            //        Text = x.CountryName,
            //        Value = x.CountryID.ToString(),
            //        Selected = x.CountryID == ot.CountryID ? true : false
            //    }),

            //};
            return View(ot);
        }

        [HttpPost]
        public ActionResult EditTeam(TeamsC objT)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    int id = repo.UpdateTeam(objT);
                    if (id > 0)
                    {
                        return RedirectToAction("TeamsList");
                    }
                }
                else
                {
                    LoadCountryList(objT.CountryID);
                }
            }
            catch(DataException ex)
            {
                ModelState.AddModelError("Error", "Ex: " + ex.Message.ToString());
            }
            return View(objT);
        }


        #endregion

        [HttpPost]
        public ActionResult SearchTeam(FormCollection FC)
        {
            if(FC.HasKeys())
            {
                if(FC["Name"]!=null)
                {
                    return View("SearchTeam", "Your name is : " + "Arsenal");
                }
            }
            return View();
        }

        
        public ActionResult SearchTeam()
        {
            return View();
        }

        
        public ActionResult DeleteTeam(int id,bool? saveError)
        {
            if (saveError.GetValueOrDefault())
            {
                if (ModelState.IsValid)
                {
                    var res = repo.GetTeamByID(id);
                    return View(res);
                }
            }
            return View();          
        }
        [HttpPost,ActionName("DeleteTeam")]       
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (id.ToString() != null)
                {
                    int idres = repo.DeleteTeamByID(id);
                    return RedirectToAction("TeamsList");
                }
            }
            catch(DataException Exception)
            {
                ModelState.AddModelError("", "Exception:" + Exception.Message.ToString());
            }
            return View();

        }



        #region For ListBox Demo
        [HttpGet]
        public ActionResult ListBoxDemo1()
        {
            List<CountryC> ctry = repo.GetCountryList();
            ListBoxDemoCountries obj = new ListBoxDemoCountries();
            obj.Cities = ctry.Select(p => new SelectListItem() {Text=p.CountryName,Value=p.CountryID.ToString() });
            return View(obj);
        }
        [HttpPost]
        public string ListBoxDemo1(IEnumerable<string> strA)
        {
            string str = string.Join(",", strA);
            return str.ToString();
        }
        #endregion


        [HttpPost]
        public ActionResult ClientValidationDemo(FormCollection FC)
        {
            return View();
        }


        public ActionResult ShowTeamDetails(string teamID)
        {
            if (Request.IsAjaxRequest())
            {
                if (teamID != null)
                {
                    var T = repo.GetTeamByID(Convert.ToInt32(teamID));
                    return View("_ShowTeamDetails", T);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }



        public ActionResult ShowTeamDetails()
        {
            if (Request.IsAjaxRequest())
            {                
               var T = repo.GetTeamByID(2);
               return View("_ShowTeamDetails", T);               
            }
            else
            {
                return View();
            }
        }

        public ActionResult Dyno()
        {
            return View();
        }

        public JsonResult GetCountryList_Json()
        {
            List<CountryC> res = repo.GetCountryList();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public string GetAjaxResult()
        {
            string str = "Hello User,This is result of the getdata by AJAX method";
            return str;
        }
        


    }
}
