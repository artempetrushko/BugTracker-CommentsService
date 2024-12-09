namespace BugTracker_CommentsService.WebApplication.Models
{
    public class CommentResponse
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public Guid AuthorId { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAtTime { get; set; }
        public DateTime? UpdatedAtTime { get; set; }
    }
}
