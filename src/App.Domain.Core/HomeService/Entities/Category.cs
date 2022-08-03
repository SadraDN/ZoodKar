namespace App.Domain.Core.HomeService.Entities
{
    public partial class Category
    {
        public Category()
        {
            Services = new HashSet<Service>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<Service> Services { get; set; }
    }
}
