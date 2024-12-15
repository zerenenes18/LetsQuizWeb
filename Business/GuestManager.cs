using Core.Utilities.Results;
using DataAccess.Abstract;
using LetsQuizCore.Entities;

namespace Business;

public class GuestManager
{
    IGuestDal _guestDal;
    public GuestManager(IGuestDal guestDal)
    {
        _guestDal = guestDal;
    }
    public IDataResult<List<Guest>> GetAll()
    {
        try
        {
            var data = _guestDal.GetAllAsync().GetAwaiter().GetResult();
            return new SuccessDataResult<List<Guest>>(data, "Guest list successfully retrieved");
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<Guest>>(null, ex.Message);
        }

    }
    public IResult Delete(Guest guest)
    {
        _guestDal.DeleteAsync(guest).GetAwaiter().GetResult();
        return new SuccessResult();
    }

    public IDataResult<Guest> GetById(Guid id)
    {
        var guest = _guestDal.GetAllAsync(x=> x.Id == id).GetAwaiter().GetResult();
        return new SuccessDataResult<Guest>(guest.FirstOrDefault(), "Guest successfully retrieved");
    }

    public IResult Add(Guest guest)
    {
        _guestDal.AddAsync(guest).GetAwaiter().GetResult();
        return new SuccessResult("Guest successfully added.");
    }
}