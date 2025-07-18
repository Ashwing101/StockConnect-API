using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRespository _CommentRespository;
        public CommentController(ICommentRespository CommentRespository)
        {
            _CommentRespository = CommentRespository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllComment()
        {
            var comments = await _CommentRespository.GetAllAsynch();

            var CommentDto = comments.Select(s => s.ToCommentDto());

            return Ok(CommentDto);

        }

      [HttpPost]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _CommentRespository.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());

        }


    }
}