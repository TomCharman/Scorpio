using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScorpioData.Dtos;
using ScorpioData.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScorpioAPI.Controllers
{
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public IEnumerable<CommentDto> Get()
        {
            var comments = _commentService.GetComments();
            return comments;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public ActionResult<CommentDto> Post([FromBody] CommentDto comment)
        {
            return _commentService.PostComment(comment);
        }

        [HttpPut("{id}/vote")]
        public async Task<ActionResult<CommentDto?>> UpvoteCommentAsync(int id){
            return await _commentService.UpvoteCommentAsync(id);
        }
    }
}

