using System.Text;
using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Infrastructure.Helper;
using ArasvaAssignment.Persistence.Contexts;
using ArasvaAssignment.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ArasvaAssignment.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddInterfaceServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // DB Context
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // Repositories
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IBorrowTransactionsRepository, BorrowTransactionRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IBookCopyRepository, BookCopyRepository>();

            // Helpers
            services.AddScoped<JwtHelper>(); 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])
                        )
                    };
                });

            return services;
        }
    }
}

