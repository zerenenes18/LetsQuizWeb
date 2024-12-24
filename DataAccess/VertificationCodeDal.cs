using Core.Entities;
using Core.EntityFramework;
using DataAccess.EntityFramework;

namespace DataAccess;

public class VerificationCodeDal : EfEntityRepositoryBase<VerificationCode, LetsQuizDbContext>
{
    public VerificationCodeDal(LetsQuizDbContext context) : base(context)
    {
    }

}