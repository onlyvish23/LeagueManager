using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeagueManager.Models
{
    public class HomeModel
    {


    }

    public class CountryListing
    {
        public string SelectedCountryID { get; set; }
        public IEnumerable<Country> CountryList
        {
            get            
            { 
                DataClasses1DataContext DC= new DataClasses1DataContext();
                IEnumerable<Country> ctry = DC.Countries;
                return ctry;
            }
        }
    }

    


}