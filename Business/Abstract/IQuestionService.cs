using Core.Utilities.Results;
using LetsQuizCore.Entities;

namespace Business.Abstract;

public interface IQuestionService
{
    Task<IDataResult<List<Question>>> GetAllAsync();
    Task<IResult> DeleteAsync(Question Question);
    Task<IDataResult<Question>> GetByIdAsync(Guid id);
   
    Task<IResult> AddAsync(Question question);
    public Task<IDataResult<List<Question>>> GetQuestionsByQuizIdAsync(Guid quizId);
    public Task<IDataResult<List<Question>>> GetQuestionsByQuizNameAsync(string quizName);


}