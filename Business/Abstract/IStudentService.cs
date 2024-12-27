using Core.Utilities.Results;
using LetsQuizCore.Entities;

namespace Business.Abstract;

public interface IStudentService
{
    Task<IDataResult<List<Student>>> GetAllAsync();
    Task<IResult> DeleteAsync(Student student);
    Task<IDataResult<Student>> GetByIdAsync(Guid id);
    Task<IResult> AddAsync(Student student);
    
}