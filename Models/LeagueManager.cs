using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LeagueManager.Models
{
    public class LeagueManager
    {
    }

    public class PlayersC
    {
        public int PlayerID { get; set; }

        [Required(ErrorMessage="First name is needed")]
        [StringLength(50,ErrorMessage="Cannot exceed more than 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is needed")]
        [StringLength(50, ErrorMessage = "Cannot exceed more than 50 characters")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Age required")]
        [Range(10,120,ErrorMessage="Age needs to be proper bro.")]
        public int? Age { get; set; }
        public string Position { get; set; }
        public int? JerseyNo { get; set; }
        public int TeamID { get; set; }
        
        [Required(ErrorMessage = "Country required")]
        public int CountryID { get; set; }
        public DateTime CreatedDate { get; set; }  
    }

    public class TeamsC
    {
        public int TeamID { get; set; }
        [Required(ErrorMessage = "Team name required")]
        public string TeamName { get; set; }
        [Required(ErrorMessage = "Country ID required")]
        public int CountryID { get; set; }
        public DateTime? Established { get; set; }
        
        public string CountryName { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }

        
    }

    public class TransfersC
    {
        public int ID { get; set; }
        public int PlayerID { get; set; }
        public int FromTeamID { get; set; }
        public int ToTeamID { get; set; }
        public DateTime TransferDate { get; set; }
        public decimal Money { get; set; }       
    }

    public class Positions
        {
            public int PositionID {get;set;}
            public string PositionName{get;set;}
        }

    public class CountryC
        {
            
            public int CountryID { get; set; }

            [Required(ErrorMessage="Country name required")]
            public string CountryName { get; set; }
        }


    public class MailerModel
    {
        public string EmailFrom = System.Web.Configuration.WebConfigurationManager.AppSettings["EmailFrom"].ToString();
        public string EmailPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["EmailPassword"].ToString();
        public string EmailPort = System.Web.Configuration.WebConfigurationManager.AppSettings["EmailPort"].ToString();
        public string EmailHost = System.Web.Configuration.WebConfigurationManager.AppSettings["EmailHost"].ToString();
        public string EnableSsl = System.Web.Configuration.WebConfigurationManager.AppSettings["EnableSsl"].ToString();

        [Required(ErrorMessage="Please enter send to email")]
        public string SendTo {get;set;}
        [Required(ErrorMessage="Please enter subject")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Please enter mail content")]
        public string Body { get; set; }        
    }

    public class CountryListForDropDown
    {
        public int SelectedCountryID { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
    }

    public class TeamsC2
    {
        public int TeamID { get; set; }
        [Required(ErrorMessage = "Team name required")]
        public string TeamName { get; set; }
        public DateTime? Established { get; set; }

        [Required(ErrorMessage = "Country ID required")]
        public int CountryID { get; set; }        
        public IEnumerable<SelectListItem> CountryList { get; set; }
    }

    public class ListBoxDemoCountries
    {
        public IEnumerable<string> SelectedCities { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
    }

    

         
}