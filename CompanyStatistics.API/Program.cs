using Autofac;
using Autofac.Extensions.DependencyInjection;
using CompanyStatistics.API.AutofacModules;
using CompanyStatistics.API.Extensions;
using CompanyStatistics.Domain.Paths;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(autofacBuilder =>
    {
        autofacBuilder.RegisterModule<ServicesModule>();
        autofacBuilder.RegisterModule<RepositoriesModule>();
    });

builder.Services.AddControllers();

builder.Services.Configure<FilesFolderPath>(builder.Configuration.GetSection(nameof(FilesFolderPath)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCompanyStatisticsAutomapper();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
