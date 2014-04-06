using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Security;
using System.Security.Principal;

namespace LeagueManager.Models
{
    public class CustomAuth
    {
    }


    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public CustomPrincipal(string UserName)
        {
            this.Identity = new GenericIdentity(UserName);
        }

        
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastLoginDateTime { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string[] MyRoles { get; set; }

        public bool IsInRole(string strRole)
        {
            return MyRoles.Contains(strRole) ? true : false;
        }
    }

    public class CustomPrincipalS 
    {
        
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public DateTime LastLoginDateTime { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string[] UserRoles { get; set; }
        public string RoleName { get; set; }
        public bool IsInRole(string strRole)
        {
            return UserRoles.Contains(strRole) ? true : false;
        }
    }
}