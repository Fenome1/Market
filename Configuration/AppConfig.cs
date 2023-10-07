using Autofac;
using MarketSolo.Modules;

namespace MarketSolo.Configuration;

public static class AppConfig
{
    static AppConfig()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule<AppModule>();
        Container = builder.Build();
    }

    public static IContainer Container { get; private set; }
}