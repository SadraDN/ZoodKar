namespace App.EndPoints.UI.Areas.Admin.Models.ViewModels.UserManagment
{
    public class UserOutputVM
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string UserName { set; get; }
        public string Email { set; get; }
        public List<string?> Roles { set; get; }
    }
}
