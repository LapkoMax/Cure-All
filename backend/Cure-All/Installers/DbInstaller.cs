using Cure_All.DataAccess;
using Cure_All.DataAccess.Repository;
using Cure_All.DataAccess.Repository.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cure_All.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Cure-All.DataAccess")));
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }
    }
}
