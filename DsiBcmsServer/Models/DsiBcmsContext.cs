using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DSI.BcmsServer.Models;

namespace DSI.BcmsServer.Models {

    public class DsiBcmsContext : DbContext {

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public virtual DbSet<Config> Configs { get; set; }

        public DsiBcmsContext(DbContextOptions<DsiBcmsContext> context) : base(context) {}

        protected override void OnModelCreating(ModelBuilder builder) {
            logger.Trace($".OnModelCreating()");
            builder.Entity<Config>(e => {
                e.ToTable("Configs");
                e.HasKey(x => x.KeyValue);
                e.Property(x => x.KeyValue).HasMaxLength(50).IsRequired();
                e.Property(x => x.DataValue).HasMaxLength(80);
                e.Property(x => x.System);
                e.Property(x => x.Active).IsRequired();
                e.Property(x => x.Created).IsRequired();
                e.Property(x => x.Updated);
                e.HasIndex(x => x.KeyValue).IsUnique();
            });
            builder.Entity<Role>(e => {
                e.ToTable("Roles");
                e.HasKey(x => x.Code);
                e.Property(x => x.Code).HasMaxLength(8).IsRequired();
                e.Property(x => x.Name).HasMaxLength(30).IsRequired();
                e.Property(x => x.IsAdmin).IsRequired();
                e.Property(x => x.IsStaff).IsRequired();
                e.Property(x => x.IsInstructor).IsRequired();
                e.Property(x => x.IsStudent).IsRequired();
                e.Property(x => x.Active).IsRequired();
                e.Property(x => x.Created).IsRequired();
                e.Property(x => x.Updated);
                e.HasIndex(x => x.Code);
            });
            builder.Entity<User>(e => {
                e.ToTable("Users");
                e.HasKey(x => x.Id);
                e.Property(x => x.Username).HasMaxLength(30).IsRequired();
                e.Property(x => x.Password).HasMaxLength(50).IsRequired();
                e.Property(x => x.Firstname).HasMaxLength(30).IsRequired();
                e.Property(x => x.Lastname).HasMaxLength(30).IsRequired();
                e.Property(x => x.CellPhone).HasMaxLength(12);
                e.Property(x => x.WorkPhone).HasMaxLength(12);
                e.Property(x => x.RoleCode).HasMaxLength(8);
                e.Property(x => x.SecurityKey).HasMaxLength(36);
                e.Property(x => x.Active).IsRequired();
                e.Property(x => x.Created).IsRequired();
                e.Property(x => x.Updated);
                e.HasIndex(x => x.Username).IsUnique();
                e.HasOne(x => x.Role).WithMany(x => x.Users).HasForeignKey(x => x.RoleCode).OnDelete(DeleteBehavior.SetNull);
            });
            builder.Entity<Cohort>(e => {
                e.ToTable("Cohorts");
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).HasMaxLength(30).IsRequired();
                e.Property(x => x.InstructorId);
                e.Property(x => x.BeginDate);
                e.Property(x => x.EndDate);
                e.Property(x => x.Capacity);
                e.HasKey(x => x.Id);
                e.HasOne(x => x.Instructor).WithMany(x => x.Cohorts).HasForeignKey(x => x.InstructorId).OnDelete(DeleteBehavior.NoAction);
                e.HasMany(x => x.Enrollments).WithOne(x => x.Cohort);
            });
            builder.Entity<Enrollment>(e => {
                e.ToTable("Enrollments");
                e.HasKey(x => new { x.UserId, x.CohortId } );
                e.Property(x => x.UserId).IsRequired();
                e.Property(x => x.CohortId).IsRequired();
                e.HasOne(x => x.User).WithMany(x => x.Enrollments).HasForeignKey(x => x.UserId);
                e.HasOne(x => x.Cohort).WithMany(x => x.Enrollments).HasForeignKey(x => x.CohortId);
            });
        }

        public DbSet<DSI.BcmsServer.Models.User> User { get; set; }

        public DbSet<DSI.BcmsServer.Models.Role> Role { get; set; }

        public DbSet<DSI.BcmsServer.Models.Cohort> Cohort { get; set; }
    }
}
