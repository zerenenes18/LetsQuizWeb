using Core.EntityFramework;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using LetsQuizCore.Entities;

namespace DataAccess
{
    public class StudentDal  : EfEntityRepositoryBase<Student,LetsQuizDbContext>, IStudentDal
    {
    }
}
