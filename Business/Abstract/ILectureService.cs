using Core.Utilities.Results;
using LetsQuizCore.Entities;

namespace Business.Abstract;

public interface ILectureService
{
    Task<IDataResult<List<Lecture>>> GetAllAsync();
    Task<IResult> DeleteAsync(Lecture userOperationClaim);
    Task<IDataResult<Lecture>> GetByIdAsync(Guid id);
    Task<IDataResult<Lecture>> GetByNameAsync(string name);
    Task<IResult> AddAsync(Lecture lecture);
    Task<IDataResult<List<Lecture>>> GetAdminLecturesAsync();
    

}