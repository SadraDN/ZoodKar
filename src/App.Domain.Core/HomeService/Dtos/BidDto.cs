namespace App.Domain.Core.HomeService.Dtos
{
    public class BidDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ExpertUserId { get; set; }
        public int SuggestedPrice { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
