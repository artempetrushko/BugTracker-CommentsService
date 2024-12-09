namespace BugTracker_CommentsService.Domain
{
    public class Comment : BaseEntity
    {
        public Guid TaskId { get; set; }
        public Guid AuthorId { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAtTime { get; set; }
        public DateTime? UpdatedAtTime { get; set; }
    }
}
