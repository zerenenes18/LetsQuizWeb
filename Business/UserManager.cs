using Business.Abstract;
using Core.Utilities.Results;
using LetsQuizCore.Entities;

namespace Business;

public class UserManager : IUserService
{
    public IDataResult<List<User>> GetAll()
    {
        throw new NotImplementedException();
    }

    public IResult Delete(User product)
    {
        throw new NotImplementedException();
    }

    public IDataResult<User> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public IResult Add(User product)
    {
        throw new NotImplementedException();
    }
}