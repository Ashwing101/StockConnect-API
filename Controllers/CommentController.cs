using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Dtos.Stock;
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
        private readonly IStockRepository _StockRepository;
        public CommentController(ICommentRespository CommentRespository, IStockRepository stockRepo)
        {
            _CommentRespository = CommentRespository;
            _StockRepository = stockRepo;
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

        [HttpPost("{stockId}")]
        public async Task<IActionResult> create([FromRoute] int stockId, CreateCommentDto commentDto)
        {

            if (!await _StockRepository.StockExist(stockId))
            {

                return BadRequest("Stock not avialable");
            }

            var commentModel = commentDto.ToCommentFromCreate(stockId);
            await _CommentRespository.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel }, commentModel.ToCommentDto());




        }


        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {

            var comment = await _CommentRespository.updateAsync(id, updateDto.ToCommentFromUpate());

            if (comment == null)
            {
                NotFound("Comment Not Found");
            }
            return Ok(comment.ToCommentDto());

        }

    }
}