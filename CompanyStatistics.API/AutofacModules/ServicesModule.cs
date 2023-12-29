using Autofac;
using CompanyStatistics.Domain.Services;
using Microsoft.AspNetCore.Authentication;

namespace CompanyStatistics.API.AutofacModules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(CompanyService).Assembly;

            builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.Name.EndsWith("Service"))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
        }
    }
}
