using Core.Utilities.Results;
using LetsQuizCore.Entities;

namespace Business.Abstract;

public interface IUserService
{
    IDataResult<List<User>> GetAll();
    IResult Delete(User user);
    IDataResult<User> GetById(Guid id);
    IResult Add(User user);
}