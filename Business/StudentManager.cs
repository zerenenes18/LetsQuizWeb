using Business.Abstract;
using Core.Utilities.Results;
using DataAccess;
using DataAccess.Abstract;
using LetsQuizCore.Entities;

namespace Business;

public class StudentManager : IStudentService
{
    IStudentDal _studentDal;
    public StudentManager(IStudentDal studentDal)
    {
        _studentDal = studentDal;
    }
    public IDataResult<List<Student>> GetAll()
    {
        try
        {
            var data = _studentDal.GetAllAsync().GetAwaiter().GetResult();
            return new SuccessDataResult<List<Student>>(data, "Admin list successfully retrieved");
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<Student>>(null, ex.Message);
        }

    }
    public IResult Delete(Student student)
    {
        _studentDal.DeleteAsync(student).GetAwaiter().GetResult();
        return new SuccessResult();
    }

    public IDataResult<Student> GetById(Guid id)
    {
        var admin = _studentDal.GetAllAsync(x=> x.Id == id).GetAwaiter().GetResult();
        return new SuccessDataResult<Student>(admin.FirstOrDefault(), "Admin successfully retrieved");
    }

    public IResult Add(Student student)
    {
        _studentDal.AddAsync(student).GetAwaiter().GetResult();
        return new SuccessResult("Admin successfully added.");
    }
}
