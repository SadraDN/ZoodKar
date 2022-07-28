using App.Domain.Core.HomeService.Entities;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Core.User.Entities;

public class AppUser : IdentityUser<int>
{
    public string? Name { get; set; } = null!;
    public bool? IsActive { get; set; } 
    public int? PictureFileId { get; set; } 
    public string? HomeAddress { get; set; } = null!;

    public virtual List<AppFile> AppFiles { get; set; }
    public virtual List<ExpertFavoriteCategory> ExpertFavoriteCategories { get; set; }
    public virtual List<Bid> Bids { get; set; }
    public virtual List<Order> CustomerOrders { get; set; }
    public virtual List<Order> ExpertOrders { get; set; }

}