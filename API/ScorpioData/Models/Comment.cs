using System;
using System.ComponentModel.DataAnnotations.Schema;
using ScorpioData.Dtos;

namespace ScorpioData.Models
{
	public class Comment
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Text { get; set; }
		public int UserId { get; set; }
		public DateTime PostedDate { get; set; }

		public User User { get; set; }
		public List<Vote> Votes { get; set; }

		public CommentDto ToDto()
        {
			return new CommentDto
			{
				Id = Id,
				Text = Text,
				UserId = UserId,
				User = User?.ToDto(),
				PostedDate = PostedDate,
				VoteCount = Votes?.Count,
			};
        }
	}
}

