namespace BugTracker_CommentsService.WebApplication.Models
{
    public class CreateCommentRequest
    {
        public Guid TaskId { get; set; }
        public Guid AuthorId { get; set; }
        public string? Content { get; set; }
    }
}
