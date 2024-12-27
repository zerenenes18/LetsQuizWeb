using Core.Entities;
using Core.EntityFramework;
using DataAccess.Abstract;
using DataAccess.EntityFramework;

namespace DataAccess;

public class OperationClaimDal : EfEntityRepositoryBase<OperationClaim,LetsQuizDbContext>, IOperationClaimDal
{
    public OperationClaimDal(LetsQuizDbContext context) : base(context)
    {
    }

}

