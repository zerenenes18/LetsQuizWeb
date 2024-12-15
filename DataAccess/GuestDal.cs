using Core.EntityFramework;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using LetsQuizCore.Entities;

namespace DataAccess;

public class GuestDal : EfEntityRepositoryBase<Guest,LetsQuizDbContext>, IGuestDal
{
    public GuestDal(LetsQuizDbContext context) : base(context)
    {
    }

}