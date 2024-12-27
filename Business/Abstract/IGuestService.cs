using Core.Utilities.Results;
using LetsQuizCore.Entities;

namespace Business.Abstract;

public interface IGuestService
{
    Task<IDataResult<List<Guest>>> GetAllAsync();
    Task<IResult> DeleteAsync(Guest guest);
    Task<IDataResult<Guest>> GetByIdAsync(Guid id);
    Task<IResult> AddAsync(Guest guest);
}