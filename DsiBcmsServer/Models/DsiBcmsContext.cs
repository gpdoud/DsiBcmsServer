using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DSI.BcmsServer.Models;

namespace DSI.BcmsServer.Models {

    public class DsiBcmsContext : DbContext {

        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public DbSet<Log> Logs { get; set; }
        public DbSet<KbCategory> KbCategories { get; set; }
        public DbSet<Kb> Kbs { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Cohort> Cohorts { get; set; }
        public DbSet<Config> Configs { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Commentary> Commentary { get; set; }
        public DbSet<InstructorCohort> InstructorCohorts { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<CalendarDay> CalendarDays { get; set; }

        public DsiBcmsContext(DbContextOptions<DsiBcmsContext> context) : base(context) {
        }

        protected override void OnModelCreating(ModelBuilder builder) {
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
                e.Property(x => x.IsRoot).IsRequired();
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
                e.Property(x => x.Email).HasMaxLength(80);
                e.Property(x => x.CellPhone).HasMaxLength(12);
                e.Property(x => x.WorkPhone).HasMaxLength(12);
                e.Property(x => x.RoleCode).HasMaxLength(8);
                e.Property(x => x.SecurityKey).HasMaxLength(36);
                e.Property(x => x.PinCode).HasMaxLength(4);
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
                e.Property(x => x.DemoDay);
                e.Property(x => x.Capacity);
                e.HasKey(x => x.Id);
                e.HasMany(x => x.Enrollments).WithOne(x => x.Cohort);
                //e.HasOne(x => x.Calendar)
                //    .WithOne(x => x.Cohort);
                //    .HasForeignKey("Cohort", "CalendarId");
            });
            builder.Entity<Enrollment>(e => {
                e.ToTable("Enrollments");

                e.HasKey(x => x.Id );
                e.Property(x => x.UserId).IsRequired();
                e.Property(x => x.CohortId).IsRequired();
                e.HasOne(x => x.User).WithMany(x => x.Enrollments).HasForeignKey(x => x.UserId);
                e.HasOne(x => x.Cohort).WithMany(x => x.Enrollments).HasForeignKey(x => x.CohortId);
                e.HasIndex(x => new { x.UserId, x.CohortId }).IsUnique();
            });
            builder.Entity<Attendance>(e => {
                e.ToTable("Attendance");

                e.HasKey(x => x.Id);
                e.Property(x => x.In);
                e.Property(x => x.Out);
                e.Property(x => x.Excused);
                e.Property(x => x.Note);
                e.HasOne(x => x.Enrollment).WithMany(x => x.Attendances).HasForeignKey(x => x.EnrollmentId);
            });
            builder.Entity<Assessment>(e => {
                e.ToTable("Assessments");

                e.HasKey(x => x.Id);
                e.Property(x => x.Date);
                e.Property(x => x.Subject);
                e.Property(x => x.Description);
                e.Property(x => x.PointsScore);
                e.Property(x => x.PointsMax);
                e.HasOne(x => x.Enrollment).WithMany(x => x.Assessments).HasForeignKey(x => x.EnrollmentId);
            });
            builder.Entity<Feedback>(e => {
                e.ToTable("Feedbacks");

                e.HasKey(x => x.Id);
                e.Property(x => x.UserId).IsRequired();
                e.Property(x => x.Category);
                e.Property(x => x.Title);
                e.Property(x => x.Text);
                e.Property(x => x.Response);
                e.Property(x => x.Locked).IsRequired();
                e.Property(x => x.NextId);
                e.Property(x => x.PrevId);
                e.Property(x => x.Active).IsRequired();
                e.Property(x => x.Created).IsRequired();
                e.Property(x => x.Updated);
                e.HasOne(x => x.User).WithMany(x => x.Feedbacks).HasForeignKey(x => x.UserId);
            });
            builder.Entity<KbCategory>(e => {
                e.ToTable("KbCategories");
                e.HasIndex(x => x.Code).IsUnique();
            });
            builder.Entity<Commentary>(e => {
                e.ToTable("Commentaries");
                e.HasKey(x => x.Id);
                e.Property(x => x.StudentId).IsRequired();
                e.Property(x => x.Text).IsRequired();
                e.Property(x => x.Sensitive).IsRequired();
                e.Property(x => x.Created).IsRequired();
                e.Property(x => x.LastAccessUserId);
                e.Property(x => x.Updated);
                e.HasOne(x => x.Student).WithMany(x => x.StudentCommentaries).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.LastAccessUser).WithMany(x => x.LastAccessUserCommentaries).HasForeignKey(x => x.LastAccessUserId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<InstructorCohort>(e => {
                e.ToTable("InstructorCohorts");
                e.HasKey(x => x.Id);
                e.Property(x => x.InstructorId).IsRequired();
                e.HasOne(x => x.Instructor)
                    .WithMany(x => x.InstructorCohorts)
                    .HasForeignKey(x => x.InstructorId)
                    .OnDelete(DeleteBehavior.NoAction);
                e.Property(x => x.CohortId).IsRequired();
                e.HasOne(x => x.Cohort)
                    .WithMany(x => x.InstructorCohorts)
                    .HasForeignKey(x => x.CohortId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Calendar>(e => {
                e.ToTable("Calendars");
                e.HasKey(x => x.Id);
                e.Property(x => x.Description).HasMaxLength(255);
                e.Property(x => x.StartDate);
                e.Property(x => x.EndDate);
                e.Property(x => x.GraduationDate);
                e.Property(x => x.Active).IsRequired();
                e.Property(x => x.Created);
                e.Property(x => x.Updated);
            });

            builder.Entity<CalendarDay>(e => {
                e.ToTable("CalendarDays");
                e.HasKey(x => x.Id);
                e.Property(x => x.Date);
                e.Property(x => x.Notes);
                e.Property(x => x.Topic);
                e.Property(x => x.Subtopic);
                e.Property(x => x.WeekNbr);
                e.Property(x => x.DayNbr);
                e.Property(x => x.AssessmentToday).HasMaxLength(50);
                e.Property(x => x.NoClassToday);
                e.HasOne(x => x.Calendar)
                    .WithMany(x => x.CalendarDays)
                    .HasForeignKey(x => x.CalendarId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
