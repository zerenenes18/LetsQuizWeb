using Core.Utilities.Results;
using LetsQuizCore.Entities;

namespace Business.Abstract;

public interface IGuestService
{
    IDataResult<List<Guest>> GetAll();
    IResult Delete(Guest guest);
    IDataResult<Guest> GetById(Guid id);
    IResult Add(Guest guest);
}