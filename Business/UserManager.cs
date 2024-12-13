using Business.Abstract;
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

    public IResult Add(User user)
    {
        _userDal.AddAsync(user).GetAwaiter().GetResult();
        return new SuccessResult("Admin successfully added.");
    }
}