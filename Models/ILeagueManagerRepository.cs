using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeagueManager.Models
{
    public interface ILeagueManagerRepository
    {

        IEnumerable<PlayersC> GetPlayersList();
        PlayersC GetPlayerByID(int ID);
        int AddPlayer(PlayersC P);
        int DeletePlayer(int PlayerID);
        int UpdatePlayer(PlayersC P);

        List<Positions> GetPositions();

       

        List<CountryC> GetCountryList();

        IEnumerable<TeamsC> GetTeamsList();
        TeamsC GetTeamByID(int ID);
        int AddTeam(TeamsC T);
        int DeleteTeamByID(int TeamID);
        int UpdateTeam(TeamsC T);

        
        int AddCountry(CountryC P);
        int DeleteCountryByID(int CountryID);
        //int UpdateCountry(CountryC P);

        IEnumerable<SelectListItem> GetRolesList();
        string GetRoleNameByID(int roleID);
        IList<User> GetUsersList();
        string DeleteUser(int id);

        UserProfile GetUserDetailsByID(int ID);
        string EditUser(UserProfile UP);
    }
}