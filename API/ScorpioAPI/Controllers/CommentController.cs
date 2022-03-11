using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScorpioData.Dtos;
using ScorpioData.Services;
using Microsoft.AspNetCore.Mvc;
using ScorpioAPI.Hubs.Clients;
using Microsoft.AspNetCore.SignalR;
using ScorpioAPI.Hubs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScorpioAPI.Controllers
{
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IHubContext<UpvoteHub, IUpvoteClient> _upvoteHub;

        public CommentController(ICommentService commentService, IHubContext<UpvoteHub, IUpvoteClient> upvoteHub)
        {
            _commentService = commentService;
            _upvoteHub = upvoteHub;
        }

        [HttpGet]
        public IEnumerable<CommentDto> Get()
        {
            var comments = _commentService.GetComments();
            return comments;
        }

        [HttpPost]
        public ActionResult<CommentDto> Post([FromBody] CommentDto comment)
        {
            return _commentService.PostComment(comment);
        }

        [HttpPut("{id}/vote")]
        public async Task<ActionResult<CommentDto?>> UpvoteCommentAsync(int id){
            var updatedComment = await _commentService.UpvoteCommentAsync(id);
            if (updatedComment != null)
            {
                await _upvoteHub.Clients.All.ReceiveMessage(new Models.UpvoteMessage
                {
                    CommentId = updatedComment.Id,
                    VoteCount = updatedComment.VoteCount ?? 0,
                });
            }
            return updatedComment;
        }
    }
}

