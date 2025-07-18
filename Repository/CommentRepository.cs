using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Migrations;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRespository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment?> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<List<Comment>> GetAllAsynch()
        {
            return await _context.Comments.ToListAsync();
         }

        public async Task<Comment?> GetByIdAsync(int id)
        { 
             return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment?> updateAsync(int id, Comment updateCommentDto)
        {
            var existingComment = await _context.Comments.FindAsync(id);

            if (existingComment == null)
            {
                return null;
            }
            existingComment.Title = updateCommentDto.Title;
            existingComment.Content = updateCommentDto.Content;

            
                await _context.SaveChangesAsync();

                return existingComment;
            }
    
    }
}