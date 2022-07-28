namespace App.EndPoints.UI.Areas.Admin.Models.ViewModels.Order
{
    public class OrderUpdateVM
    {
        public int Id { get; set; }
        public int ServiceBasePrice { get; set; }
        public byte StatusId { get; set; }
    }
}
