using Autofac;
using MarketSolo.Data;

namespace MarketSolo.Modules;

public class AppModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MarketDbContext>().AsSelf().InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(ThisAssembly)
            .Where(prop => prop.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .InstancePerDependency();

        builder.RegisterAssemblyTypes(ThisAssembly)
            .Where(prop => prop.Name.EndsWith("ViewModel"))
            .AsSelf()
            .InstancePerDependency();
    }
}