using Microsoft.AspNetCore.Identity;

namespace App.Domain.Core.Entities;

public class AppUser : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsActive { get; set; }
    public int PictureFileId { get; set; }
    public string HomeAddress { get; set; }

    public virtual ICollection<AppFile> AppFiles { get; set; }
    public virtual ICollection<ExpertFavoriteCategory> ExpertFavoriteCategorys { get; set; }
    public virtual ICollection<Bid> Bids { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}