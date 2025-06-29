﻿using Microsoft.Extensions.DependencyInjection;

namespace Colabs.ProjectManagement.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies
                (AppDomain.CurrentDomain.GetAssemblies()));
            
            return services;
        }
    }
}
