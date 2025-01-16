using Business.Abstract;
using Business.Constants;
using Business.Operations;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using LetsQuizCore.Entities;

namespace Business;

public class UserManager : IUserService
{
    IUserDal _userDal;
    private readonly IUserOperationClaimService _userOperationClaimService;

    public UserManager(IUserDal userDal,IUserOperationClaimService userOperationClaimService)
    {
        _userDal = userDal;
        _userOperationClaimService = userOperationClaimService;
        
    }
    
   
    public async Task<IDataResult<List<User>>> GetAllAsync()
    {
        try
        {
            var data =await  _userDal.GetAllAsync();
            return new SuccessDataResult<List<User>>(data, "Admin list successfully retrieved");
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<User>>(null, ex.Message);
        }

    }
    public async Task<IResult> DeleteAsync(User user)
    {
        await _userDal.DeleteAsync(user);
        return new SuccessResult();
    }

    public async Task<IDataResult<User>> GetByIdAsync(Guid id)
    {
        var user = await _userDal.GetAllAsync(x => x.Id == id);
        return new SuccessDataResult<User>(user.FirstOrDefault(), "Admin successfully retrieved");
    }

    public  async Task<IDataResult<User>> GetByMailAsync(string email)
    {
        User user = await _userDal.GetAsync(u => u.Email == email);
        if (user != null)
        {
            return new SuccessDataResult<User>(user,Messages.UserAlreadyExists);
        }
        else
        {
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }
       
    }

    public async Task<IDataResult<User>> GetByUserNameAsync(string userName)
    {
        User user = await _userDal.GetAsync(u => u.UserName == userName);
        if (user != null)
        {
            return new SuccessDataResult<User>(user,Messages.UserAlreadyExists);
        }
        else
        {
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }
    }

    public async Task<IResult> AddAsync(User user)
    {
        await _userDal.AddAsync(user);
        UserOperationClaim userOperationClaim = new UserOperationClaim
        {
            UserId = user.Id
        };
        _userOperationClaimService.AddAsync(userOperationClaim, user.Role);
        
        return new SuccessResult("Admin successfully added.");
    }

    public async Task<IResult> UpdateAsync(User user)
    {
        await _userDal.UpdateAsync(user);
        return new SuccessResult();
    }

  
    public async Task<IDataResult<List<OperationClaim>>> GetClaimsAsync(User user)
    {
        return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user)); 
    }
}