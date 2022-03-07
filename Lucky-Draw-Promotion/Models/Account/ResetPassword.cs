using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lucky_Draw_Promotion.Models.Account
{
    public class ResetPassword
    {
        //[Required(ErrorMessage = "Kiểm tra lại mật khẩu của bạn")]
        //[StringLength(20, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 6)]
        //[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9_])", ErrorMessage = "Kiểm tra lại mật khẩu của bạn")]
        public string Password { get; set; }

        //[Compare("Password", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không khớp.")]

        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
