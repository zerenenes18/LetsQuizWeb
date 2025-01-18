using Core.EntityFramework;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using LetsQuizCore.Entities;

namespace DataAccess;

public class UserOptionDal : EfEntityRepositoryBase<UserOption,LetsQuizDbContext>, IUserOptionDal
{
public UserOptionDal(LetsQuizDbContext context) : base(context)
{
}

}