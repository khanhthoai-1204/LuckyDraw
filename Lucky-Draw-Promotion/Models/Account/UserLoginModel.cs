using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lucky_Draw_Promotion.Models.Account
{
    public class UserLoginModel
    {
        //[Required(ErrorMessage = "Kiểm tra lại Email của bạn")]
        public string Email { get; set; }
        //[StringLength(20, ErrorMessage = "Must be between 6 and 20 characters", MinimumLength = 6)]
        //[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9_])", ErrorMessage = "Kiểm tra lại mật khẩu của bạn")]
        public string Password { get; set; }
        public bool RememberMe { get; internal set; }
       

    }
}
