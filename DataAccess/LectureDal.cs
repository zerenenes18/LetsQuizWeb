using Core.EntityFramework;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using LetsQuizCore.Entities;

namespace DataAccess
{
    public class LectureDal : EfEntityRepositoryBase<Lecture,LetsQuizDbContext>, ILectureDal
    {
        
        
    }
}
