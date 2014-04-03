using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Web.Mvc;

namespace LeagueManager.Models
{
    

    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            if (roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string[] roles { get; set; }
    }



    //public abstract class BaseViewPage : WebViewPage
    //{
    //    public virtual new CustomPrincipal User
    //    {
    //        get { return base.User as CustomPrincipal; }
    //    }
    //}

    public class CustomPrincipalSerializeModel
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        
        public string[] roles { get; set; }
    }

    
}