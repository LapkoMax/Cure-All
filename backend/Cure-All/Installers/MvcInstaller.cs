using Cure_All.BusinessLogic.AutoMapper;
using Cure_All.BusinessLogic.Options;
using Cure_All.BusinessLogic.Services;
using Cure_All.BusinessLogic.Services.Impl;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Cure_All.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddMediatR(Assembly.GetExecutingAssembly());

            var emailOptions = configuration.GetSection("EmailOptions").Get<EmailOptions>();
            services.AddSingleton(emailOptions);

            services.AddScoped<IEmailService, EmailService>();

            services.AddCors();
            services.AddControllers();
        }
    }
}
