using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Lucky_Draw_Promotion.Models.Account
{
    public class User : IdentityUser
    {

       
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "mật khẩu không trùng")]
        public string ConfirmPassword { get; set; }
    }
}
