using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.UI.Models.Account
{
    public class LoginViewModel
    {


        [Required]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "من را به خاطر بسپار")]
        public bool RememberMe { get; set; }

    }
}

