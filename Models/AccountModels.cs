using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace LeagueManager.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("LeagueConnectionString")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("Users")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        
        [Required]
        
        public string UserName { get; set; }
        [Required]
        
        public string FirstName { get; set; }
        [Required]
        
        public string LastName { get; set; }
        [Required]
        
        public string Mobile { get; set; }
        [Required]
        
        public string EmailAddress { get; set; }
        
        [Required]
        public  int RoleID { get; set; }
        public virtual string RoleName { get; set; }


        //public bool? IsCoachingNow { get; set; }

        
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Cannot exceed more than 50 chars")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Cannot exceed more than 50 chars")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Cannot exceed more than 50 chars")]
        public string Mobile { get; set; }
        [Required]
        [MaxLength(500, ErrorMessage = "Email Cannot exceed more than 500 chars")]
        public string EmailAddress { get; set; }


        [Required]
        public int RoleID { get; set; }
        
        public virtual string RoleName { get; set; }


        public bool IsCoachingNow { get; set; }


        [MustBeTrue(ErrorMessage = "Please agree to the terms.")]
        public virtual bool IsTermAccepted { get; set; }

        public IEnumerable<SelectListItem> GetRolesList
        {
            get
            {
                ILeagueManagerRepository IL = new LeagueManagerRepository();
                return IL.GetRolesList();
            }
        }

    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }


    //************************************************************************************************

    public class UserRole
    { 
        [MustBeSelected(ErrorMessage="Please select role for new user")]
        public int? RoleID{get;set;}
        public string RoleName { get; set; }

    }

    /// <summary>
    /// My custom validation
    /// </summary>
    public class MustBeTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is bool && (bool)value;
        }

    }

    public class MustBeSelectedAttribute : ValidationAttribute, IClientValidatable // IClientValidatable for client side Validation
    {
        public override bool IsValid(object value)
        {
            if (value == null || (int)value == 0)
                return false;
            else
                return true;
        }
        // Implement IClientValidatable for client side Validation
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new ModelClientValidationRule[] { new ModelClientValidationRule { ValidationType = "dropdown", ErrorMessage = this.ErrorMessage } };
        }
    }
}
