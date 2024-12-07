using Core.EntityFramework;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using LetsQuizCore.Entities;

namespace DataAccess
{
    public class QuestionDal : EfEntityRepositoryBase<Question,LetsQuizDbContext>, IQuestionDal
    {
    }
}
