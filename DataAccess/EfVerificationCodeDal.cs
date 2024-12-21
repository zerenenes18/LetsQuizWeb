using Core.Entities;
using Core.EntityFramework;
using DataAccess.EntityFramework;

namespace DataAccess;

public class EfVerificationCodeDal: EfEntityRepositoryBase<VerificationCode, LetsQuizDbContext>
{
    public EfVerificationCodeDal(LetsQuizDbContext context) : base(context)
    {
    }
    
}