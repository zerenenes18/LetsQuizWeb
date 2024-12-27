using Business.Abstract;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business;

public class OperationClaimManager : IOperationClaimService
{
    IOperationClaimDal _operationClaimDal;
    public OperationClaimManager(IOperationClaimDal operationClaimDal)
    {
        _operationClaimDal  = operationClaimDal;
    }
    public async Task<IDataResult<List<OperationClaim>>> GetAllAsync()
    {
        var result =  _operationClaimDal.GetAllAsync();
        return new SuccessDataResult<List<OperationClaim>>( );
    }

    public async Task<IResult> DeleteAsync(OperationClaim operationClaim)
    {
       _operationClaimDal.DeleteAsync(operationClaim);
       return new SuccessResult();
    }

    public async Task<IDataResult<OperationClaim>> GetByIdAsync(Guid id)
    {
      return new SuccessDataResult<OperationClaim>(await _operationClaimDal.GetAsync(c=> c.Id == id));
    }

    public async Task<IDataResult<OperationClaim>> GetByNameAsync(string name)
    {
        return new SuccessDataResult<OperationClaim>( await _operationClaimDal.GetAsync(c=> c.Name == name));
    }

    public async Task<IResult> AddAsync(OperationClaim operationClaim)
    {
        await _operationClaimDal.AddAsync(operationClaim);
        return new SuccessResult();
    }
}