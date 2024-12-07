using Business.Abstract;
using Core.Utilities.Results;
using LetsQuizCore.Entities;

namespace Business;

public class AdminManager : IAdminService
{
    public IDataResult<List<Admin>> GetAll()
    {
        throw new NotImplementedException();
    }

    public IResult Delete(Admin product)
    {
        throw new NotImplementedException();
    }

    public IDataResult<Admin> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public IResult Add(Admin product)
    {
        throw new NotImplementedException();
    }
}