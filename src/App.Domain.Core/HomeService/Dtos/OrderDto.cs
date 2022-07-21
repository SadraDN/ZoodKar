namespace App.Domain.Core.HomeService.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public byte StatusId { get; set; }
        public int ServiceId { get; set; }
        public int ServiceBasePrice { get; set; }
        public int? CustomerUserId { get; set; }
        public int? FinalExpertUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual List<AppFileDto> AppFiles { get; set; } = new List<AppFileDto>();
    }
}
