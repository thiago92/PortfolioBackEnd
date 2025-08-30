using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplica todas as configurações de entidades do assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        // Adicione DbSets das suas entidades aqui
        public DbSet<Domain.Entities.User> Users { get; set; }
        public DbSet<Domain.Entities.Role> Roles { get; set; }
        public DbSet<Domain.Entities.Permission> Permissions { get; set; }
        public DbSet<Domain.Entities.RolePermission> RolePermissions { get; set; }
        public DbSet<Domain.Entities.Skill> Skills { get; set; }
        public DbSet<Domain.Entities.Work> Works { get; set; }
        public DbSet<Domain.Entities.WorkSkill> WorkSkills { get; set; }
    }
}
