using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeagueManager.Models
{
    public class LeagueManagerRepository : ILeagueManagerRepository
    {
        private DataClasses1DataContext DC;

        public LeagueManagerRepository()
        {
            DC = new DataClasses1DataContext();
        }

        public IEnumerable<PlayersC> GetPlayersList()
        {
            IList<PlayersC> pList = new List<PlayersC>();

            var res = from P in DC.Players
                      select P;
            if(res!=null && res.Count()>0)
            {
                foreach(var X in res)
                {
                    pList.Add(new PlayersC() 
                    { PlayerID=X.PlayerID,FirstName=X.FirstName,
                        LastName=X.LastName,Position=X.Position,
                        Age=(int)X.Age,CountryID=(int)X.CountryID,
                        CreatedDate= (DateTime)X.CreatedDate
                    });                
                }
            }
            return pList;
        }

        public PlayersC GetPlayerByID(int ID)
        {

            Player P = DC.Players.SingleOrDefault(p => p.PlayerID == ID);
            var model = new PlayersC()
            {
                PlayerID = ID,
                FirstName = P.FirstName,
                LastName = P.LastName,
                Age = P.Age,
                CountryID = (int)P.CountryID,
                TeamID = (int)P.TeamID,               
                CreatedDate= (DateTime)P.CreatedDate
            };
            return model;
        }

        public int AddPlayer(PlayersC P)
        {
            int i = 0;
            try
            {
                var userData = new Player()
                {
                    FirstName = P.FirstName,
                    LastName = P.LastName,
                    Age = P.Age,CountryID=P.CountryID,CreatedDate=P.CreatedDate,
                    Position= P.Position,JerseyNo=P.JerseyNo ,
                    TeamID = P.TeamID
                };
                DC.Players.InsertOnSubmit(userData);
                DC.SubmitChanges();
                i = userData.PlayerID;
            }
            catch (Exception ex)
            {
                string str = ex.Message.ToString();
            }
            return i;
        }

        public int DeletePlayer(int PlayerID)
        {
            int i = 0;
            try
            {
                Player Player = DC.Players.Where(u => u.PlayerID == PlayerID).SingleOrDefault();
                if (Player != null)
                {
                    DC.Players.DeleteOnSubmit(Player);
                    DC.SubmitChanges();
                    i = 1;
                }
            }
            catch(Exception ex)
            {
                string str = ex.Message.ToString();
            }
            return i;
        }

        public int UpdatePlayer(PlayersC P)
        {
            int i = 0;
            try
            {
                Player P_up = DC.Players.Where(u => u.PlayerID == P.PlayerID).SingleOrDefault();
                P_up.FirstName = P.FirstName;
                P_up.LastName = P.LastName;
                P_up.Age = P.Age;
                P_up.TeamID = P.TeamID;
                P_up.Position = P.Position;
                P_up.JerseyNo = P.JerseyNo;
                P_up.CountryID = P.CountryID;
                DC.SubmitChanges();
                i = 1;
            }
            catch (Exception ex)
            {
                string str = ex.Message.ToString();
            }
            return i;
        }

        public List<Positions> GetPositions()
        {

            List<Positions> positions = new List<Positions>()
            {
                new Positions() { PositionID = 1, PositionName = "GoalKeeper"},
                new Positions() { PositionID = 2, PositionName = "Defender"},
                new Positions() { PositionID = 3, PositionName = "Midfielder"},
                new Positions() { PositionID = 4, PositionName = "Striker"}
            };
            return positions;
        }


        public IEnumerable<TeamsC> GetTeamsList()
        {
            
            var teams = from T in DC.Teams
                        join C in DC.Countries on T.CountryID equals C.CountryID
                        select new TeamsC
                        {
                            TeamID=T.TeamID,
                            TeamName=T.TeamName,
                            Established=T.Established,
                            CountryID= C.CountryID,
                            CountryName= C.CountryName
                        };
            return (IEnumerable<TeamsC>)teams;
        }

        public List<CountryC> GetCountryList()
        {
            //List<CountryC> ctry = new List<CountryC>()
            //{
            //    new CountryC(){CountryID=1,CountryName="USA"},
            //    new CountryC(){CountryID=2,CountryName="England"},
            //    new CountryC(){CountryID=3,CountryName="India"}
            //};
            var ctry = (from C in DC.Countries
                        select new CountryC                       
                        {
                            CountryID= C.CountryID,
                            CountryName=C.CountryName
                        }).ToList<CountryC>();
            return ctry;
        }

        public int AddTeam(TeamsC T)
        {
            int ID = 0;
            Team newT = new Team { 
                TeamName = T.TeamName,
                CountryID=T.CountryID,
                CreatedDate = DateTime.Now,
                Established = T.Established };
            DC.Teams.InsertOnSubmit(newT);
            DC.SubmitChanges();
            ID = newT.TeamID;
            return ID;
        }

        public int UpdateTeam(TeamsC T)
        {
            int Id = 0;
            Team objT = DC.Teams.SingleOrDefault(p=>p.TeamID==T.TeamID);
            if(objT!=null)
            {
                objT.TeamName = T.TeamName;
                objT.Established = T.Established;
                objT.CreatedDate = DateTime.Now;
                objT.CountryID = T.CountryID;
                DC.SubmitChanges();
                Id = 1;
            }
            return Id;
        }

        public int AddCountry(CountryC C)
        {
            int ID = 0;
            Country newT = new Country { CountryName= C.CountryName};
            DC.Countries.InsertOnSubmit(newT);
            DC.SubmitChanges();
            ID = newT.CountryID;
            return ID;
        }

        public int DeleteCountryByID(int CountryID)
        {
            int Id = 0;
            Country objC = DC.Countries.SingleOrDefault(p=>p.CountryID== CountryID);
            if(objC!=null)
            {
                DC.Countries.DeleteOnSubmit(objC);
                DC.SubmitChanges();
                Id = 1;
            }
            return Id;
        }

        public int DeleteTeamByID(int TeamID)
        {
            int id = 0;
            Team objT = DC.Teams.SingleOrDefault(p=>p.TeamID==TeamID);
            if(objT!=null)
            {
                DC.Teams.DeleteOnSubmit(objT);
                DC.SubmitChanges();
                id = 1;
            }
            return id;
        }

        public TeamsC GetTeamByID(int ID)
        {
            var res = (from T in DC.Teams
                      join C in DC.Countries on T.CountryID equals C.CountryID
                      where T.TeamID== ID
                      select new TeamsC
                      { TeamID=T.TeamID,TeamName= T.TeamName,Established=T.Established,
                          CountryID=(int)T.CountryID,
                          CountryName=C.CountryName }).SingleOrDefault<TeamsC>();

            return res;
        }


        public IEnumerable<SelectListItem> GetRolesList()
        { 
             DataClasses1DataContext DC = new DataClasses1DataContext();
             DC.ObjectTrackingEnabled = false;
             IEnumerable<SelectListItem> list = (from R in DC.webpages_Roles
                                                 select new SelectListItem
                                                 {
                                                     Value = R.RoleId.ToString(),
                                                     Text = R.RoleName
                                                 }).AsEnumerable<SelectListItem>();
             return list;
        }

        public string GetRoleNameByID(int roleID)
        { 
            string str="";
            using(DataClasses1DataContext DC = new DataClasses1DataContext())
            {
                str = (from R in DC.webpages_Roles
                       where R.RoleId == roleID
                       select R.RoleName).SingleOrDefault().ToString();

            }
            return str;
        }

        public IList<User> GetUsersList()
        {
            DataClasses1DataContext DC = new DataClasses1DataContext();
            DC.ObjectTrackingEnabled = false;
            var res = (from U in DC.Users
                       select U).ToList<User>();
            return res;
        }

        public string DeleteUser(int id)
        {
            string str = "";
            DataClasses1DataContext DC = new DataClasses1DataContext();
            User U = DC.Users.SingleOrDefault(p => p.UserID == id);
            if(U!=null)
            {
                DC.Users.DeleteOnSubmit(U);
                DC.SubmitChanges();
                str = "Success";
            }
            return str;
        }

        public string EditUser(UserProfile UP)
        {
            string str = "";
            DataClasses1DataContext DC = new DataClasses1DataContext();
            User U = DC.Users.SingleOrDefault(p => p.UserID == UP.UserId);
            if (U != null)
            {
                //User UP = new UserProfile();
                //U.UserID = UP.UserId;
                U.FirstName = UP.FirstName;
                U.LastName = UP.LastName;
                U.UserName = UP.UserName;
                //U.RoleID = UP.RoleID;
                U.EmailAddress = UP.EmailAddress;
                U.Mobile = UP.Mobile;
                DC.SubmitChanges();
                str = "Success";
            }
            return str;
        }



        public UserProfile GetUserDetailsByID(int ID)
        {
            DataClasses1DataContext DC = new DataClasses1DataContext();
            DC.ObjectTrackingEnabled = false;

            User U = DC.Users.SingleOrDefault(p=>p.UserID==ID);
            UserProfile UP = new UserProfile();
            //UP.UserId = U.UserID;
            UP.FirstName = U.FirstName;
            UP.LastName = U.LastName;
            UP.UserName = U.UserName;
            UP.Mobile= U.Mobile;
            //UP.RoleID = (int)U.RoleID;
            UP.EmailAddress = U.EmailAddress;

            return UP;            
        }



    }
}