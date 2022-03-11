using System;
using ScorpioAPI.Models;

namespace ScorpioAPI.Hubs.Clients
{
	public interface IUpvoteClient
	{
		Task ReceiveMessage(UpvoteMessage message);
	}
}

