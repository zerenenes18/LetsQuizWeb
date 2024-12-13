using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using LetsQuizCore.Entities;

namespace Business;

public class AdminManager : IAdminService
{
    IAdminDal _adminDal;
    public AdminManager(IAdminDal adminDal)
    {
        _adminDal = adminDal;
    }
    public IDataResult<List<Admin>> GetAll()
    {
        try
        {
            var data = _adminDal.GetAllAsync().GetAwaiter().GetResult();
            return new SuccessDataResult<List<Admin>>(data, "Admin list successfully retrieved");
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<Admin>>(null, ex.Message);
        }

    }
    public IResult Delete(Admin admin)
    {
        _adminDal.DeleteAsync(admin).GetAwaiter().GetResult();
        return new SuccessResult();
    }

    public IDataResult<Admin> GetById(Guid id)
    {
        var admin = _adminDal.GetAllAsync(x=> x.Id == id).GetAwaiter().GetResult();
        return new SuccessDataResult<Admin>(admin.FirstOrDefault(), "Admin successfully retrieved");
    }

    public IResult Add(Admin admin)
    {
        _adminDal.AddAsync(admin).GetAwaiter().GetResult();
      return new SuccessResult("Admin successfully added.");
    }
}