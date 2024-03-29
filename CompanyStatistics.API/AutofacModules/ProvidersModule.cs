﻿using Autofac;
using CompanyStatistics.Domain.DTOs;

namespace CompanyStatistics.API.AutofacModules
{
    public class ProvidersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(BaseResponseDto).Assembly;

            builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.Name.EndsWith("Provider"))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
        }
    }
}
