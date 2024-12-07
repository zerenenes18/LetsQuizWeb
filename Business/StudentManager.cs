using Business.Abstract;
using Core.Utilities.Results;
using LetsQuizCore.Entities;

namespace Business;

public class StudentManager : IStudentService
{
    public IDataResult<List<Student>> GetAll()
    {
        throw new NotImplementedException();
    }

    public IResult Delete(Student product)
    {
        throw new NotImplementedException();
    }

    public IDataResult<Student> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public IResult Add(Student product)
    {
        throw new NotImplementedException();
    }
}
