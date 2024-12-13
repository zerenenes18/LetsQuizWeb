using Core.EntityFramework;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using LetsQuizCore.Entities;

namespace DataAccess
{
    public class UserDal : EfEntityRepositoryBase<User,LetsQuizDbContext>, IUserDal
    {
        public UserDal(LetsQuizDbContext context) : base(context)
        {
        }

    }
}
