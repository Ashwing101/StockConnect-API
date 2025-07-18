using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {

            return new CommentDto
            {

                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };

        }

        public static Comment ToCommentFromCreate(this CreateCommentDto createCommentDto, int StockId)
        {

            return new Comment
            {
                Title = createCommentDto.Title,
                Content = createCommentDto.Content,
                StockId = StockId
            };

        }
        
            public static Comment ToCommentFromUpate(this UpdateCommentRequestDto updateCommentDto)
        {

            return new Comment
            {
                Title = updateCommentDto.Title,
                Content = updateCommentDto.Content
    
            };

        }
        
    }
}