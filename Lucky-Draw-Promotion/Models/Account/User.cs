using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Lucky_Draw_Promotion.Models.Account
{
    public class User : IdentityUser
    {

      
        //[StringLength(20, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 6)]
        //[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9_])", ErrorMessage = "Kiểm tra lại mật khẩu của bạn")]
        public string Password { get; set; }
      
        public string ConfirmPassword { get; set; }
    }
}
