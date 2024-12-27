using Core.Utilities.Results;
using LetsQuizCore.Entities;

namespace Business.Abstract;

public interface IQuizService
{
    Task<IDataResult<List<Quiz>>> GetAllAsync();
    Task<IResult> DeleteAsync(Quiz userOperationClaim);
    Task<IDataResult<Quiz>> GetByIdAsync(Guid id);
    Task<IDataResult<Quiz>> GetByNameAsync(string name);
    Task<IResult> AddAsync(Quiz lecture);
    public Task<IDataResult<List<Quiz>>> GetAdminQuizzesAsync();
    Task<IResult> AddToAdminAsync(string quizName,Guid lectureId);
    
}