﻿using Autofac;
using CompanyStatistics.Infrastructure.Entities;
using Module = Autofac.Module;

namespace CompanyStatistics.API.AutofacModules
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(BaseEntity).Assembly;

            builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Work"))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
        }
    }
}
