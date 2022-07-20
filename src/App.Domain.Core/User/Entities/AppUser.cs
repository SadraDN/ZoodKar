using App.Domain.Core.BaseData.Entities;
using App.Domain.Core.HomeService.Entities;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Core.User.Entities;

public class AppUser : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsActive { get; set; }
    public int PictureFileId { get; set; }
    public string HomeAddress { get; set; }

    public virtual List<AppFile> AppFiles { get; set; }
    public virtual List<ExpertFavoriteCategory> ExpertFavoriteCategories { get; set; }
    public virtual List<Bid> Bids { get; set; }
    public virtual List<Order> Orders { get; set; }
}