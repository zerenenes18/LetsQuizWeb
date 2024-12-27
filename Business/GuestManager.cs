using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using LetsQuizCore.Entities;

namespace Business;

public class GuestManager : IGuestService
{
    IGuestDal _guestDal;
    public GuestManager(IGuestDal guestDal)
    {
        _guestDal = guestDal;
    }
    public async Task<IDataResult<List<Guest>>> GetAllAsync()
    {
        try
        {
            var data = await _guestDal.GetAllAsync();
            return new SuccessDataResult<List<Guest>>(data, "Guest list successfully retrieved");
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<Guest>>(null, ex.Message);
        }
    }
    public async Task<IResult> DeleteAsync(Guest guest)
    {
        await _guestDal.DeleteAsync(guest);
        return new SuccessResult();
    }

    public async Task<IDataResult<Guest>> GetByIdAsync(Guid id)
    {
        var guest = await _guestDal.GetAllAsync(x => x.Id == id);
        return new SuccessDataResult<Guest>(guest.FirstOrDefault(), "Guest successfully retrieved");
    }

    public async Task<IResult> AddAsync(Guest guest)
    {
        await _guestDal.AddAsync(guest);
        return new SuccessResult("Guest successfully added.");
    }
}