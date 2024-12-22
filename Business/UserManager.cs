using Business.Abstract;
using Business.Constants;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using LetsQuizCore.Entities;

namespace Business;

public class UserManager : IUserService
{
    IUserDal _userDal;
    public UserManager(IUserDal userDal)
    {
        _userDal = userDal;
    }
    public IDataResult<List<User>> GetAll()
    {
        try
        {
            var data = _userDal.GetAllAsync().GetAwaiter().GetResult();
            return new SuccessDataResult<List<User>>(data, "Admin list successfully retrieved");
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<User>>(null, ex.Message);
        }

    }
    public IResult Delete(User user)
    {
        _userDal.DeleteAsync(user).GetAwaiter().GetResult();
        return new SuccessResult();
    }

    public IDataResult<User> GetById(Guid id)
    {
        var user = _userDal.GetAllAsync(x=> x.Id == id).GetAwaiter().GetResult();
        return new SuccessDataResult<User>(user.FirstOrDefault(), "Admin successfully retrieved");
    }

    public  IDataResult<User> GetByMail(string email)
    {
        User user = _userDal.GetAsync(u => u.Email == email).GetAwaiter().GetResult();
        if (user != null)
        {
            return new SuccessDataResult<User>(user,Messages.UserAlreadyExists);
        }
        else
        {
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }
       
    }

    public IResult Add(User user)
    {
        _userDal.AddAsync(user).GetAwaiter().GetResult();
        return new SuccessResult("Admin successfully added.");
    }

    public IDataResult<List<OperationClaim>> GetClaims(User user)
    {
        var claims = _userDal.GetClaims(user);
        if (claims.Count == 0)
        {
            return new SuccessDataResult<List<OperationClaim>>(claims);
        }
        else
        {
            return new ErrorDataResult<List<OperationClaim>>(Messages.NotFound);
        }
        
    }
}