namespace App.Domain.Core.HomeService.Entities
{
    public partial class Entity
    {
        public Entity()
        {
            Files = new HashSet<AppFile>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<AppFile> Files { get; set; }
    }
}
