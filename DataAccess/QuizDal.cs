using Core.EntityFramework;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using LetsQuizCore.Entities;

namespace DataAccess
{
    public class QuizDal  : EfEntityRepositoryBase<Admin,LetsQuizDbContext>, IAdminDal
    {
        public QuizDal(LetsQuizDbContext context) : base(context)
        {
        }

    }
}
