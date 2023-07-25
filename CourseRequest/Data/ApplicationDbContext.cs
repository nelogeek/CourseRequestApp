using Microsoft.EntityFrameworkCore;
using CourseRequest.Models;

namespace CourseRequest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<TempUser> TempUser { get; set; }
        public DbSet<TempDept> TempDept { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Определение связи между таблицами Users и Roles через поле RoleId
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);
        }
    }

    // Модель данных для таблицы Users
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; } // Внешний ключ для связи с таблицей Roles
        public Role Role { get; set; } // Навигационное свойство для доступа к роли пользователя
    }

    // Модель данных для таблицы Roles
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }

}
