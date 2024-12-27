using Core.EntityFramework;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using LetsQuizCore.Entities;

namespace DataAccess
{
    public class QuizDal  : EfEntityRepositoryBase<Quiz,LetsQuizDbContext>, IQuizDal
    {
        public QuizDal(LetsQuizDbContext context) : base(context)
        {
        }

    }
}
