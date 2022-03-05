using System;
using System.ComponentModel.DataAnnotations.Schema;
using ScorpioData.Dtos;

namespace ScorpioData.Models
{
	public class User
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string? Name { get; set; }

		public UserDto ToDto()
		{
			return new UserDto
			{
				Id = Id,
				Name = Name,
			};
		}
	}
}

