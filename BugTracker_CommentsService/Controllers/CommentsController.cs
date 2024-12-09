using BugTracker_CommentsService.DAL.Abstractions;
using BugTracker_CommentsService.Domain;
using BugTracker_CommentsService.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker_CommentsService.WebApplication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly IRepository<Comment> _commentsRepository;

        public CommentsController(IRepository<Comment> commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

        //TODO: сделать xml-комменты для Swagger
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CommentResponse>> GetCommentByIdAsync(Guid id)
        {
            var comment = await _commentsRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return new CommentResponse()
            {
                Id = comment.Id,
                TaskId = comment.IssueId,
                AuthorId = comment.AuthorId,
                Content = comment.Content,
                CreatedAtTime = comment.CreatedAtTime,
                UpdatedAtTime = comment.UpdatedAtTime,
            };
        }

        [HttpGet("taskComments/{id:guid}")]
        public async Task<ActionResult<List<CommentResponse>>> GetAllCommentsByIssueId(Guid id)
        {
            var comments = await _commentsRepository.GetAllAsync();
            var taskComments = comments
                .Where(c => c.IssueId == id)
                .ToList();
            return taskComments
                .Select(taskComment => new CommentResponse()
                {
                    Id = taskComment.IssueId,
                    TaskId = taskComment.IssueId,
                    AuthorId = taskComment.AuthorId,
                    Content = taskComment.Content,
                    CreatedAtTime = taskComment.CreatedAtTime,
                    UpdatedAtTime = taskComment.UpdatedAtTime
                })
                .ToList();
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentRequest request)
        {
            var comment = new Comment()
            {
                Id = Guid.NewGuid(),
                IssueId = request.TaskId,
                AuthorId = request.AuthorId,
                Content = request.Content,
                CreatedAtTime = DateTime.UtcNow,
                UpdatedAtTime = DateTime.UtcNow,
            };
            await _commentsRepository.AddAsync(comment);
            await _commentsRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> EditComment(Guid id, UpdateCommentRequest request)
        {
            var comment = await _commentsRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            comment.Content = request.Content;
            comment.UpdatedAtTime = DateTime.UtcNow;

            _commentsRepository.Update(comment);
            await _commentsRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            await _commentsRepository.RemoveAsync(id);
            await _commentsRepository.SaveChangesAsync();
            return Ok();
        }
    }
}
