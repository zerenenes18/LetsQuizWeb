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
    public async Task<IDataResult<List<Student>>> GetAllAsync()
    {
        try
        {
            var data = await _studentDal.GetAllAsync();
            return new SuccessDataResult<List<Student>>(data, "Admin list successfully retrieved");
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<Student>>(null, ex.Message);
        }

    }
    public async Task<IResult> DeleteAsync(Student student)
    {
        await _studentDal.DeleteAsync(student);
        return new SuccessResult();
    }

    public async Task<IDataResult<Student>> GetByIdAsync(Guid id)
    {
        var admin = await _studentDal.GetAllAsync(x => x.Id == id);
        return new SuccessDataResult<Student>(admin.FirstOrDefault(), "Admin successfully retrieved");
    }

    public async Task<IResult> AddAsync(Student student)
    {
        await _studentDal.AddAsync(student);
        return new SuccessResult("Admin successfully added.");
    }
}
