using Core.Entities;
using Core.EntityFramework;
using DataAccess.Abstract;
using DataAccess.EntityFramework;

namespace DataAccess;

public class UserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim,LetsQuizDbContext>, IUserOperationClaimDal
{
    public UserOperationClaimDal(LetsQuizDbContext context) : base(context)
    {
    }
    
}