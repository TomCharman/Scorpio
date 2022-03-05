using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScorpioData.Models
{
	public class Vote
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int UserId { get; set; }
		public int CommentId { get; set; }

		public User User { get; set; }
		public Comment Comment { get; set; }
	}
}

