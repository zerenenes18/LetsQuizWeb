using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using LetsQuizCore.Entities;

namespace Business;

public class AdminManager : IAdminService
{
    private readonly IAdminDal _adminDal;

    public AdminManager(IAdminDal adminDal)
    {
        _adminDal = adminDal;
    }

    public async Task<IDataResult<List<Admin>>> GetAllAsync()
    {
        try
        {
            var data = await _adminDal.GetAllAsync();
            return new SuccessDataResult<List<Admin>>(data, "Admin list successfully retrieved");
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<Admin>>(null, ex.Message);
        }
    }

    public async Task<IResult> DeleteAsync(Admin admin)
    {
        await _adminDal.DeleteAsync(admin);
        return new SuccessResult("Admin successfully deleted.");
    }

    public async Task<IDataResult<Admin>> GetByIdAsync(Guid id)
    {
        var admin = await _adminDal.GetAllAsync(x => x.Id == id);
        return admin.FirstOrDefault() != null
            ? new SuccessDataResult<Admin>(admin.FirstOrDefault(), "Admin successfully retrieved")
            : new ErrorDataResult<Admin>("Admin not found");
    }

    public async Task<IResult> AddAsync(Admin admin)
    {
        await _adminDal.AddAsync(admin);
        return new SuccessResult("Admin successfully added.");
    }
}