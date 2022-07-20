namespace App.Domain.Core.HomeService.Dtos
{
    public class ServiceCommentDto
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int OrderId { get; set; }
        public string? CommentText { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
