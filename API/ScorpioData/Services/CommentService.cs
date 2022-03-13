using System;
using Microsoft.EntityFrameworkCore;
using ScorpioData.Dtos;
using ScorpioData.Models;

namespace ScorpioData.Services
{
	public interface ICommentService
    {
		public List<CommentDto> GetComments();
		public CommentDto PostComment(CommentDto comment);
        Task<CommentDto?> UpvoteCommentAsync(int commentId);
    }

	public class CommentService: ICommentService
	{
		private readonly Context _context;
        private readonly IUserService _userService;

		public CommentService(Context context, IUserService userService)
		{
			_context = context;
            _userService = userService;
		}

        private CommentDto? GetComment(int commentId)
        {
            return _context.Comments
                .Include(c => c.User)
                .Include(c => c.Votes)
                .Include(c => c.ChildComments)
                .ThenInclude(cc => cc.User)
                .FirstOrDefault(c => c.Id == commentId)?
                .ToDto();
        }

        public List<CommentDto> GetComments()
        {
			var comments = _context.Comments
                .OrderByDescending(c => c.PostedDate)
                .Where(c => c.ParentCommentId == null)
                .Include(c => c.User)
                .Include(c => c.ChildComments.OrderByDescending(c => c.PostedDate))
                .ThenInclude(cc => cc.User)
                .Select(c => new
                {
                    Comment = c,
                    VoteCount = c.Votes.Count,
                })
                .ToList()
                .Select(c =>
                {
                    var comment = c.Comment.ToDto();
                    comment.VoteCount = c.VoteCount;
                    return comment;
                })
                .ToList();
			return comments;
        }

        public CommentDto PostComment(CommentDto comment)
        {
            var user = _userService.Me();
            var newComment = new Comment
            {
                Text = comment.Text,
                UserId = user.Id,
                PostedDate = DateTime.UtcNow,
                ParentCommentId = comment.ParentCommentId,
            };
            _context.Comments.Add(newComment);
            _context.SaveChanges();
            return newComment.ToDto();
        }

        public async Task<CommentDto?> UpvoteCommentAsync(int commentId)
        {
            var user = _userService.Me();

            var existingVote = _context.Votes
                .FirstOrDefault(v => v.CommentId == commentId && v.UserId == user.Id);

            if (existingVote == null)
            {
                var newVote = new Vote
                {
                    CommentId = commentId,
                    UserId = user.Id,
                };

                await _context.Votes.AddAsync(newVote);
                await _context.SaveChangesAsync();
            }

            return GetComment(commentId);
        }
    }
}

