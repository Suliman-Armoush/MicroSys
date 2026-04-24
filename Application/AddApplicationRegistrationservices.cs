using Application.Features.Department.Command.Create;
using Application.Helper;
using Application.Helper.Profiles;
using AutoMapper;
using Azure;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

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
                cfg.AddProfile<RoleProfile>();
                cfg.AddProfile<SysInfoProfile>();


            });



            // تسجيل MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            services.AddControllers()
                .AddJsonOptions(options =>
                 {
                     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                 });
            return services;
        }
    }

}
