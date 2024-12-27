using Core.Entities;
using Core.Utilities.Results;
using LetsQuizCore.Entities;

namespace Business.Abstract;

public interface IUserService
{
    Task<IDataResult<List<User>>> GetAllAsync();
    Task<IResult> DeleteAsync(User user);
    Task<IDataResult<User>> GetByIdAsync(Guid id);
    Task<IDataResult<User>> GetByMailAsync(string email);
    
    Task<IDataResult<User>> GetByUserNameAsync(string userName);
    Task<IResult> AddAsync(User user);
    Task<IResult> UpdateAsync(User user);
    Task<IDataResult<List<OperationClaim>>> GetClaimsAsync(User user);
}