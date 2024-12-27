using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Constants;
using Core.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Module = Autofac.Module;
using Castle.DynamicProxy;
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
        
        builder.RegisterType<AuthManager>().As<IAuthService>();
        builder.RegisterType<JwtHelper>().As<ITokenHelper>();
       
        // builder.RegisterType<AdminManager>().As<IAdminService>().SingleInstance();
        builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
        builder.RegisterType<VerificationCodeDal>().As<VerificationCodeDal>().SingleInstance();
        builder.RegisterType<MailSettings>().As<MailSettings>().SingleInstance();
        
        builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
        builder.RegisterType<UserOperationClaimDal>().As<IUserOperationClaimDal>();
        
        builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
        builder.RegisterType<OperationClaimDal>().As<IOperationClaimDal>();
        builder.RegisterType<LectureDal>().As<ILectureDal>();
        builder.RegisterType<LectureManager>().As<ILectureService>();
        builder.RegisterType<QuizManager>().As<IQuizService>();
        builder.RegisterType<QuizDal>().As<IQuizDal>();
        
        builder.RegisterType<QuestionManager>().As<IQuestionService>();
        builder.RegisterType<QuizDal>().As<IQuizDal>();

        builder.RegisterType<QuestionDal>().As<IQuestionDal>();
        builder.RegisterType<OptionDal>().As<IOptionDal>();
        builder.RegisterType<ScoreDal>().As<IScoreDal>();

        
        builder.Register(ctx =>
            {
                var configuration = ctx.Resolve<IConfiguration>();
                var optionsBuilder = new DbContextOptionsBuilder<LetsQuizDbContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    
                return new LetsQuizDbContext(optionsBuilder.Options);
            })
            .AsSelf() // Kendi türü olarak kaydedilir.
            .As<DbContext>() // DbContext türü olarak kaydedilir.
            .InstancePerLifetimeScope(); // Her yaşam döngüsünde bir örnek oluşturulur.
        
        
        
        var assembly = Assembly.GetExecutingAssembly();
        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
    }
}