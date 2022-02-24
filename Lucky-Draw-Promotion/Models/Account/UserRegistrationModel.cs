using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static Lucky_Draw_Promotion.Models.Account.ListIdentityUser;

namespace Lucky_Draw_Promotion.Models.Account
{
    public class UserRegistrationModel
    {
        public int UserRegistrationModelID { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength((10), MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [NotMapped]
        [Compare("Password")]
        public string ComfirmPassword { get; set; }
        public RoleType Type { get; set; }
        [NotMapped]
        public List<SelectListItem> RoleTypes { get; set; }


        public UserRegistrationModel()
        {
            RoleTypes = new List<SelectListItem>();
            RoleTypes.Add(new SelectListItem
            {
                Value = ((int)RoleType.Admin).ToString(),
                Text = RoleType.Admin.ToString()
            });
            RoleTypes.Add(new SelectListItem
            {
                Value = ((int)RoleType.User).ToString(),
                Text = RoleType.User.ToString()
            });
        }
    }
}
