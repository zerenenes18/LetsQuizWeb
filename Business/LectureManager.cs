using Business.Abstract;
using Business.Operations;
using Core.Extensions;
using Core.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using LetsQuizCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using IResult = Core.Utilities.Results.IResult;

namespace Business;

public class LectureManager : ILectureService
{
  
    private readonly ILectureDal _lectureDal;
    private IHttpContextAccessor _httpContextAccessor;
    
    public LectureManager(ILectureDal lectureDal)
    {
        _lectureDal = lectureDal;
        _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
    }    
    
    public async Task<IDataResult<List<Lecture>>> GetAllAsync()
    {
        return new SuccessDataResult<List<Lecture>>( await _lectureDal.GetAllAsync());
    }

    public async Task<IResult> DeleteAsync(Lecture userOperationClaim)
    {
        throw new NotImplementedException();
    }

    public async Task<IDataResult<Lecture>> GetByIdAsync(Guid id)
    {
        return new SuccessDataResult<Lecture>(await _lectureDal.GetAsync(l => l.Id == id));
    }

    public async Task<IDataResult<Lecture>> GetByNameAsync(string name)
    {
        var adminId = Guid.Parse(_httpContextAccessor.HttpContext.User.ClaimIdentifier());
        var lectureList = await _lectureDal.GetAllAsync(l => l.AdminId == adminId);
        return new SuccessDataResult<Lecture>(lectureList.SingleOrDefault(l => l.Name == name));

    }
    

    public async Task<IResult> AddAsync(Lecture lecture)
    {
        //var test = _httpContextAccessor.HttpContext.User.Claims;
  
        var adminId = Guid.Parse(_httpContextAccessor.HttpContext.User.ClaimIdentifier());
        lecture.AdminId = adminId;
        _lectureDal.AddAsync(lecture);
        return new SuccessResult();
    }

    
    public async Task<IDataResult<List<Lecture>>> GetAdminLecturesAsync()
    {
        var adminId = Guid.Parse(_httpContextAccessor.HttpContext.User.ClaimIdentifier());
        var lecturesResult = await _lectureDal.GetAllAsync(l => l.AdminId == adminId);
        
        return new SuccessDataResult<List<Lecture>>(lecturesResult);
    }
}