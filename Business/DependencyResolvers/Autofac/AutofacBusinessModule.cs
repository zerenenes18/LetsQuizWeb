using Autofac;
using Business.Abstract;
using DataAccess;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AdminManager>().As<IAdminService>().SingleInstance();
        builder.RegisterType<AdminDal>().As<IAdminDal>().SingleInstance();

        builder.RegisterType<StudentManager>().As<IStudentService>().SingleInstance();
        builder.RegisterType<StudentDal>().As<IStudentDal>().SingleInstance();
       
        builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
        builder.RegisterType<UserDal>().As<IUserDal>().SingleInstance();
      
        builder.RegisterType<AuthService>().As<IAuthService>().SingleInstance();
       
        // builder.RegisterType<AdminManager>().As<IAdminService>().SingleInstance();

       
       
       
        // DbContext kaydı
        builder.Register(ctx =>
        {
           
            var configuration = ctx.Resolve<IConfiguration>();
        
         
            var optionsBuilder = new DbContextOptionsBuilder<LetsQuizDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new LetsQuizDbContext(optionsBuilder.Options);
        }).AsSelf().InstancePerLifetimeScope();


    }
}