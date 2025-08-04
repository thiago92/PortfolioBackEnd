using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class AddRepositoryDependencyInjection
    {
        public static void RepositorysDependencyInjection(this IServiceCollection repository)
        {
            repository.AddScoped<IPermissionRepository, PermissionRepository>();
            repository.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            repository.AddScoped<IRoleRepository, RoleRepository>();
            repository.AddScoped<ISkillRepository, SkillRepository>();
            repository.AddScoped<IUserRepository, UserRepository>();
            repository.AddScoped<IWorkRepository, WorkRepository>();
            repository.AddScoped<IWorkSkillRepository, WorkSkillRepository>();
            repository.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
