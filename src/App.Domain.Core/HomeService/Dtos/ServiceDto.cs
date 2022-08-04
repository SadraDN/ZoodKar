namespace App.Domain.Core.HomeService.Dtos
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string Title { get; set; } = null!;
        public string? ShortDescription { get; set; }
        public int Price { get; set; }
        public List<AppFileDto>? AppFiles { get; set; } = new List<AppFileDto>();
        public List<ExpertFavoriteServiceDto>? ExpertFavoriteServices { get; set; } = new List<ExpertFavoriteServiceDto>();
    }
}
