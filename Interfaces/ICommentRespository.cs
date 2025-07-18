using api.Dtos.Comment;
using api.Models;

namespace api.Interfaces
{

    public interface ICommentRespository
    {

        Task<List<Comment>> GetAllAsynch();

        Task<Comment?> GetByIdAsync(int id);

        Task<Comment?> CreateAsync(Comment commentModel);

        Task<Comment?> updateAsync(int id, Comment updateCommentDto);



    }




}