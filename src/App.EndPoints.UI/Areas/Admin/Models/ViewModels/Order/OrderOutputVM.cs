namespace App.EndPoints.UI.Areas.Admin.Models.ViewModels.Order
{
    public class OrderOutputVM
    {
        public int Id { get; set; }
        public byte StatusId { get; set; }
        public int ServiceId { get; set; }
        public int ServiceBasePrice { get; set; }
        public int? CustomerUserId { get; set; }
        public int? FinalExpertUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
