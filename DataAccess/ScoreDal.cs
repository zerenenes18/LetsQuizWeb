using Core.EntityFramework;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using LetsQuizCore.Entities;

namespace DataAccess;

public class ScoreDal : EfEntityRepositoryBase<Score,LetsQuizDbContext>, IScoreDal
{
    public ScoreDal(LetsQuizDbContext context) : base(context)
    {
    }

}