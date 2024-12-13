using Core.EntityFramework;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using LetsQuizCore.Entities;

namespace DataAccess
{
    public class OptionDal : EfEntityRepositoryBase<Option,LetsQuizDbContext>, IOptionDal
    {
        public OptionDal(LetsQuizDbContext context) : base(context)
        {
        }

    }
}
