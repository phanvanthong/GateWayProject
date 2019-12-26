using Autofac;
using GetWay.API.Modules;
using GetWay.Data.DbContext;

namespace GetWay.Modules
{
    public class ApplicaitonModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new AutoMapperModule());
            builder.RegisterType<getwayDbContext>().InstancePerLifetimeScope();
        }
    }
}