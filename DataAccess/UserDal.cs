using Core.Entities;
using Core.EntityFramework;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using LetsQuizCore.Entities;

namespace DataAccess
{
    public class UserDal : EfEntityRepositoryBase<User,LetsQuizDbContext>, IUserDal
    {
        LetsQuizDbContext _context;
        public UserDal(LetsQuizDbContext context) : base(context)
        {
            _context = context;
        }

        public List<OperationClaim> GetClaims(User user)
        {
                var result = from operationClaim in _context.OperationClaims
                    join userOperationClaim in _context.UserOperationClaims
                        on operationClaim.Id equals userOperationClaim.OperationClaimId
                    where userOperationClaim.UserId == user.Id
                    select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            
        }
    }
}
