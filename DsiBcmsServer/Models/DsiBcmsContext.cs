using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DSI.BcmsServer.Models;

namespace DSI.BcmsServer.Models {

    public class DsiBcmsContext : DbContext {

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public virtual DbSet<SystemControl> SysCtrls { get; set; }

        public DsiBcmsContext(DbContextOptions<DsiBcmsContext> context) : base(context) {}

        protected override void OnModelCreating(ModelBuilder builder) {
            logger.Trace($".OnModelCreating()");
            builder.Entity<SystemControl>(e => {
                e.HasKey(x => x.Key);
                e.Property(x => x.Key).HasMaxLength(50).IsRequired();
                e.Property(x => x.Value).HasMaxLength(80);
                e.Property(x => x.Category).HasMaxLength(30);
                e.Property(x => x.Active).IsRequired();
                e.Property(x => x.Created).IsRequired();
                e.Property(x => x.Updated);
                e.HasIndex(x => x.Key).IsUnique();
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
                e.Property(x => x.Active).IsRequired();
                e.Property(x => x.Created).IsRequired();
                e.Property(x => x.Updated);
                e.HasIndex(x => x.Username).IsUnique();
            });
        }

        public DbSet<DSI.BcmsServer.Models.User> User { get; set; }
    }
}
