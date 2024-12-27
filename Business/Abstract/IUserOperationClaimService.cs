using Core.Entities;
using Core.Utilities.Results;

namespace Business.Abstract;

public interface IUserOperationClaimService
{
    Task<IDataResult<List<UserOperationClaim>>> GetAllAsync(); 
    Task<IResult> DeleteAsync(UserOperationClaim userOperationClaim); 
    Task<IDataResult<UserOperationClaim>> GetByIdAsync(Guid id); 
    Task<IResult> AddAsync(UserOperationClaim userOperationClaim, string claimType); 
}