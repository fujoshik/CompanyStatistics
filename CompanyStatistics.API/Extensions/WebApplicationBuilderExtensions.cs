﻿using CompanyStatistics.Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CompanyStatistics.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder)
        {
            var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                });

            builder.Services.AddSingleton(new JwtSettings()
            {
                Key = jwtSettings.Key,
                Issuer = jwtSettings.Issuer,
                Audience = jwtSettings.Audience,
                ExpirationInMinutes = jwtSettings.ExpirationInMinutes
            });

            return builder;
        }
    }
}
