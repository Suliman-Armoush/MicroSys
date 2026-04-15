using Application.Features.Department.Command.Create;
using Application.Helper;
using Application.Helper.Profiles;
using AutoMapper;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Application
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // تسجيل AutoMappers
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DepartmentProfile>();
                cfg.AddProfile<UserProfile>();


            } );



            // تسجيل MediatR
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


            return services;
        }
    }
   
}
