using System;
using ScorpioData.Models;

namespace ScorpioData.Dtos
{
	public class CommentDto
	{
		public int Id { get; set; }
		public string? Text { get; set; }
		public int UserId { get; set; }
		public UserDto? User { get; set; }
		public DateTime PostedDate { get; set; }
		public int? VoteCount { get; set; }
		public int? ParentCommentId { get; set; }
		public List<CommentDto> ChildComments { get; set; }
	}
}

