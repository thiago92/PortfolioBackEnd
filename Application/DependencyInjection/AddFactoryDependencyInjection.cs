using Application.Factories;
using Application.Interfaces.IFactories;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection
{
    public static class AddFactoryDependencyInjection
    {
        public static void FactoryDependencyInjection(this IServiceCollection service)
        {
            service.AddScoped<IUserFactory, UserFactory>();
            service.AddScoped<IRoleFactory, RoleFactory>();
            service.AddScoped<IPermissionFactory, PermissionFactory>();
            service.AddScoped<IRolePermissionFactory, RolePermissionFactory>();
            service.AddScoped<ISkillFactory, SkillFactory>();
            service.AddScoped<IWorkFactory, WorkFactory>();
            service.AddScoped<IWorkSkillFactory, WorkSkillFactory>();
        }
    }
}
