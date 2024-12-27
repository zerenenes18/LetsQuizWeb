using Core.Utilities.Results;
using LetsQuizCore.Entities;

namespace Business.Abstract;

public interface IAdminService
{
    Task<IDataResult<List<Admin>>> GetAllAsync();
    Task<IResult> DeleteAsync(Admin admin);
    Task<IDataResult<Admin>> GetByIdAsync(Guid id);
    Task<IResult> AddAsync(Admin admin);
}