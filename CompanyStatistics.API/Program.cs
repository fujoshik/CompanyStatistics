using Autofac;
using Autofac.Extensions.DependencyInjection;
using CompanyStatistics.API.AutofacModules;
using CompanyStatistics.API.Extensions;
using CompanyStatistics.Domain.Paths;
using CompanyStatistics.Domain.Settings;
using CompanyStatistics.Domain.Validators.Authentication;
using FluentValidation;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(autofacBuilder =>
    {
        autofacBuilder.RegisterModule<ServicesModule>();
        autofacBuilder.RegisterModule<RepositoriesModule>();
        autofacBuilder.RegisterModule<FactoriesModule>();
        autofacBuilder.RegisterModule<ProvidersModule>();
    });

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();

builder.Services.AddControllers();

builder.Services.AddQuartzConfiguration();

builder.AddJwtAuthentication();

builder.Services.AddMemoryCache();

builder.Services.Configure<FilesFolderPath>(builder.Configuration.GetSection(nameof(FilesFolderPath)))
                .Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)))
                .Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CompanyStatistics API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Example: \"Athorization: Bearer {token}\"",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCompanyStatisticsAutomapper();

var app = builder.Build();

app.ConfigureSafelistMiddleware(builder.Configuration["AdminSafeList"]);

app.ConfigureCustomExceptionMiddleware();

app.ConfigureCustomHeaderMiddleware();

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
