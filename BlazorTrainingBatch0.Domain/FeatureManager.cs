using BlazorTrainingBatch0.Database.AppDbContextModels;
using BlazorTrainingBatch0.Domain.Features;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorTrainingBatch0.Domain
{
    public static class FeatureManager
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddScoped<IWalletRepo, WalletRepo>();
            services.AddScoped<IWalletService, WalletService>();
        }

        public static void AddDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("DbConnection"), 
                ServiceLifetime.Transient, 
                ServiceLifetime.Transient);
        }
    }
}
