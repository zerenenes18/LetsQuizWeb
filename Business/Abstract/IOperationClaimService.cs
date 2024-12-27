using Core.Entities;
using Core.Utilities.Results;

namespace Business.Abstract;

public interface IOperationClaimService
{
    Task<IDataResult<List<OperationClaim>>> GetAllAsync();
    Task<IResult> DeleteAsync(OperationClaim operationClaim);
    Task<IDataResult<OperationClaim>> GetByIdAsync(Guid id);
    Task<IDataResult<OperationClaim>> GetByNameAsync(string name);
    Task<IResult> AddAsync(OperationClaim operationClaim);

}