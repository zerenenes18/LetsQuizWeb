using Business.Abstract;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business;

public class UserOperationClaimManager : IUserOperationClaimService
{
    private readonly IUserOperationClaimDal _userOperationClaimDal;
    private readonly IOperationClaimService _operationClaimService;

    public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal, IOperationClaimService operationClaimService)
    {
        _userOperationClaimDal = userOperationClaimDal;
        _operationClaimService = operationClaimService;
    }

    public async Task<IDataResult<List<UserOperationClaim>>> GetAllAsync()
    {
        var claims = await _userOperationClaimDal.GetAllAsync();
        return new SuccessDataResult<List<UserOperationClaim>>(claims);
    }

    public async Task<IResult> DeleteAsync(UserOperationClaim userOperationClaim)
    {
        await _userOperationClaimDal.DeleteAsync(userOperationClaim);
        return new SuccessResult();
    }
    public async Task<IDataResult<UserOperationClaim>> GetByIdAsync(Guid id)
    {
        var claim = await _userOperationClaimDal.GetAsync(c => c.Id == id);
        return new SuccessDataResult<UserOperationClaim>(claim);
    }

    public async Task<IResult> AddAsync(UserOperationClaim userOperationClaim, string claimType)
    {
        claimType = claimType.ToLower(); // Önce claimType küçük harfe çevriliyor
        
        var claimResult =await _operationClaimService.GetByNameAsync(claimType);
        
        if (claimResult.Success && claimResult.Data != null)
        {
            userOperationClaim.OperationClaimId = claimResult.Data.Id;
            await _userOperationClaimDal.AddAsync(userOperationClaim);
            return new SuccessResult();
        }
        
        return new ErrorResult("Claim not found or invalid");
    }
}