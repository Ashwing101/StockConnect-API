using api.Models;

namespace api.Interfaces
{

    public interface ICommentRespository
    {

        Task<List<Comment>> GetAllAsynch();

        Task<Comment?> GetByIdAsync(int id);



    }




}