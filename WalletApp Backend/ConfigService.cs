using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WalletApp_Backend.Transactions.Commands.CreateTransactionCommand;

namespace WalletApp_Backend
{
    public static class ConfigService
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddSwagger();
            services.AddMapster();
            services.AddMediatR(option => option.RegisterServicesFromAssemblyContaining<Program>());
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            return services;
        }
        private static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(
                    swagger =>
                    {
                        swagger.AddSecurityDefinition(
                                "Bearer", new OpenApiSecurityScheme()
                                {
                                    Name = "Authorization",
                                    Type = SecuritySchemeType.ApiKey,
                                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                                    BearerFormat = "JWT",
                                    In = ParameterLocation.Header,
                                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                                }
                                );
                        swagger.AddSecurityRequirement(
                                new OpenApiSecurityRequirement
                                {
                                    {
                                        new OpenApiSecurityScheme
                                        {
                                            Reference = new OpenApiReference
                                            {
                                                Type = ReferenceType.SecurityScheme,
                                                Id = JwtBearerDefaults.AuthenticationScheme
                                            }
                                        },
                                        new string[] { }

                                    }
                                }
                                );
                    }
                    );
            services.AddSwaggerGen();
        }
        private static void AddMapster(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssemblyContaining<CreateTransactionCommandValidation>();
        }
    }
}