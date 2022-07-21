using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.UI.Areas.Admin.Models.ViewModels.UserManagment
{
    public class UserUpdateVM
    {
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [Display(Name = "آیدی")]
        public int Id { set; get; }

        [Required(ErrorMessage = "این فیلد الزامی است")]
        [Display(Name = "نام کاربری")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "این فیلد الزامی است")]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { set; get; }

        [Required(ErrorMessage = "این فیلد الزامی است")]
        [MaxLength(100, ErrorMessage = "حداکثر کارکتر {1}باشد {0} ")]
        [MinLength(5, ErrorMessage = "حداقل کارکتر {1}باشد {0} ")]
        [Display(Name = "رمز عبور")]
        public string Password { set; get; }

        [Display(Name = "تکرار رمز عبور")]
        [Compare(nameof(Password), ErrorMessage = "تکرار رمز عبور باید یکسان باشد")]
        public string ConfirmPassword { set; get; }
        public List<string?> Roles { set; get; }
    }
}
