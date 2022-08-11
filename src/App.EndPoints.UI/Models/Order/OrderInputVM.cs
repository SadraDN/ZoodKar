using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.UI.Models.Order
{
    public class OrderInputVM
    {
        [Required(ErrorMessage = "لطفا فیلد آدرس را کامل نمایید")]
        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Required(ErrorMessage ="لطفا تاریخ انجام سفارش را وارد نمایید")]
        [Display(Name = "تاریخ انجام سرویس")]
        public DateTime ServiceDate { get; set; }
    }
}
