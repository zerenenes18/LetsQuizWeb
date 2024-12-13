using Core.EntityFramework;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using LetsQuizCore.Entities;

namespace DataAccess
{
    public class AdminDal : EfEntityRepositoryBase<Admin,LetsQuizDbContext>, IAdminDal
    {
        public AdminDal(LetsQuizDbContext context) : base(context)
        {
            
        }

        
    }
}
