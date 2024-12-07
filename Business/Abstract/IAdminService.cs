using Core.Utilities.Results;
using LetsQuizCore.Entities;

namespace Business.Abstract;

public interface IAdminService
{
    IDataResult<List<Admin>> GetAll();
    IResult Delete(Admin product);
    IDataResult<Admin> GetById(Guid id);
    IResult Add(Admin product);
}