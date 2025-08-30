using Application.Interfaces.IServices;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection
{
    public static class AddServicesDependencyInjection
    {
        public static void ServicesDependencyInjection(this IServiceCollection service)
        {
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IRoleService, RoleService>();
            service.AddScoped<IPermissionService, PermissionService>();
            service.AddScoped<IRolePermissionService, RolePermissionService>();
            service.AddScoped<ISkillService, SkillService>();
            service.AddScoped<IWorkService, WorkService>();
            service.AddScoped<IWorkSkillService, WorkSkillService>();
        }
    }
}
