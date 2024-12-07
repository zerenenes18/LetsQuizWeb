using Core.Utilities.Results;
using LetsQuizCore.Entities;

namespace Business.Abstract;

public interface IStudentService
{
    IDataResult<List<Student>> GetAll();
    IResult Delete(Student product);
    IDataResult<Student> GetById(Guid id);
    IResult Add(Student product);
    
}