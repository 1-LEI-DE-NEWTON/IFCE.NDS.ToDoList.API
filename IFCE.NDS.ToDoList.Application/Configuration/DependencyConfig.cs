using FluentValidation;
using IFCE.NDS.ToDoList.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NDS_ToDo.Application.Notifications;
using NDS_ToDo.Application.Services;
using NDS_ToDo.Application.Services.Contracts;
using NDS_ToDo.Application.Validations;
using NDS_ToDo.Domain.Entities;
using ScottBrady91.AspNetCore.Identity;

namespace NDS_ToDo.Application.Configuration;

public static class DependencyCOnfig
{
    public static void ResolveDependencies(this IServiceCollection services)
    {
        services
            .AddScoped<INotificator, Notificator>()
            .AddScoped<IPasswordHasher<User>, Argon2PasswordHasher<User>>();
        services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IAssignmentService, AssignmentService>()
            .AddScoped<IAssignmentListService, AssignmentListService>();

        services
            .AddScoped<IValidator<Assignment>, AssignmentValidator>()
            .AddScoped<IValidator<AssignmentList>, AssignmentListValidator>();

        services
            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    }
}