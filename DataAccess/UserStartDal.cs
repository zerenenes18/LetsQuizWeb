using Core.EntityFramework;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using LetsQuizCore.Entities;

namespace DataAccess;

public class UserStartDal : EfEntityRepositoryBase<UserStart,LetsQuizDbContext>, IUserStartDal
{
    public UserStartDal(LetsQuizDbContext context) : base(context)
    {
    }

}

