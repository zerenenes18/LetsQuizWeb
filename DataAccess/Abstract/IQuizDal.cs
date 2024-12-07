using Core.DataAccess;
using LetsQuizCore.Entities;

namespace DataAccess.Abstract
{
    public interface IQuizDal: IEntityRepository<Quiz>
    {
    }
}
