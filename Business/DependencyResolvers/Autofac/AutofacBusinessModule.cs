using Autofac;

namespace Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
       // builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
       // builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
       // builder.RegisterType<EfVerificationCodeDal>().As<EfVerificationCodeDal>().SingleInstance();

    }
}