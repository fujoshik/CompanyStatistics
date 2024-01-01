using Autofac;
using Autofac.Extensions.DependencyInjection;
using CompanyStatistics.API.AutofacModules;
using CompanyStatistics.API.Extensions;
using CompanyStatistics.Domain.Paths;
using CompanyStatistics.Domain.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(autofacBuilder =>
    {
        autofacBuilder.RegisterModule<ServicesModule>();
        autofacBuilder.RegisterModule<RepositoriesModule>();
    });

builder.Services.AddControllers();

builder.Services.Configure<FilesFolderPath>(builder.Configuration.GetSection(nameof(FilesFolderPath)))
                .Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCompanyStatisticsAutomapper();

var app = builder.Build();

app.ConfigureSafelistMiddleware(builder.Configuration["AdminSafeList"]);

app.ConfigureCustomExceptionMiddleware();

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
