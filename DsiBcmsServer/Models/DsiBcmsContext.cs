using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DSI.BcmsServer.Models {

    public class DsiBcmsContext : DbContext {

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public virtual DbSet<SystemControl> SysCtrls { get; set; }

        public DsiBcmsContext(DbContextOptions<DsiBcmsContext> context) : base(context) {}

        protected override void OnModelCreating(ModelBuilder builder) {
            logger.Trace($".OnModelCreating()");
            builder.Entity<SystemControl>(e => {
                e.HasKey(x => x.Key);
                e.Property(x => x.Key).HasMaxLength(50).IsRequired(true);
                e.Property(x => x.Value).HasMaxLength(80);
                e.Property(x => x.Category).HasMaxLength(30);
                e.Property(x => x.Active).IsRequired(true).HasDefaultValue(true);
                e.Property(x => x.Created).IsRequired(true).ValueGeneratedOnAdd();
                e.Property(x => x.Updated).IsRequired(false).ValueGeneratedOnUpdate();
                e.HasIndex(x => x.Key).IsUnique(true);
            });
        }
    }
}
