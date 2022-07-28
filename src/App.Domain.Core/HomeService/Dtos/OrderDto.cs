namespace App.Domain.Core.HomeService.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime ServiceDate { get; set; }
        public string SerivceAddress { get; set; }
        public byte StatusId { get; set; }
        public string? StatusName { get; set; }
        public int ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public int ServiceBasePrice { get; set; }
        public int? CustomerUserId { get; set; }
        public string? CustomerUserName { get; set; }
        public int? FinalExpertUserId { get; set; }
        public string? FinalExpertUserName { get; set; }
        public DateTime CreatedAt { get; set; }
        //public virtual List<AppFileDto> AppFiles { get; set; } 
    }
}
