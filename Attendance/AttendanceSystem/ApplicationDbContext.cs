using System.Reflection;
using System.Configuration;
using AttendanceSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;

namespace AttendanceSystem
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["AttendanceSystem"].ConnectionString;

        public ApplicationDbContext()
        {
            _connectionString = connectionString;
            _migrationAssembly = Assembly.GetExecutingAssembly().GetName().Name;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString, (x) => x.MigrationsAssembly(_migrationAssembly));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollments");
            modelBuilder.Entity<Enrollment>().HasKey(x => new { x.CourseId, x.StudentId });

            modelBuilder.Entity<Enrollment>()
               .HasOne(x => x.Student)
               .WithMany(y => y.Enrollments)
               .HasForeignKey(z => z.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(x => x.Course)
                .WithMany(y => y.Enrollments)
                .HasForeignKey(z => z.CourseId);
            */
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<EntityUser> Users { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
    }
}
