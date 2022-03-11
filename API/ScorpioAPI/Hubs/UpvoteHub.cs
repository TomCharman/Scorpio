using System;
using Microsoft.AspNetCore.SignalR;
using ScorpioAPI.Hubs.Clients;
using ScorpioAPI.Models;

namespace ScorpioAPI.Hubs
{
	public class UpvoteHub : Hub<IUpvoteClient>
	{
    }
}

