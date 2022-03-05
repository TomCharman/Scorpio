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

        public List<CommentDto> GetComments()
        {
			var comments = _context.Comments
                .Include(c => c.User)
                .Select(c => c.ToDto())
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
            };
            _context.Comments.Add(newComment);
            _context.SaveChanges();
            return newComment.ToDto();
        }
    }
}

