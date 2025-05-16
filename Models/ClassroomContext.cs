using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AcademIQ.Models
{
    public class ClassroomContext : IdentityDbContext<Users>
    {

        public ClassroomContext(DbContextOptions<ClassroomContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Enrollments> Enrollments { get; set; }
        public DbSet<Assignments> Assignments { get; set; }
        public DbSet<Submissions> Submissions { get; set; }
        public DbSet<Announcements> Announcements { get; set; }
        public DbSet<Attachments> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // important for Identity

            // --- Inheritance ---
            modelBuilder.Entity<Users>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Students>("Student")
                .HasValue<Teachers>("Teacher");

            modelBuilder.Entity<Announcements>()
                .HasDiscriminator<string>("AnnouncementType")
                .HasValue<Announcements>("Announcement")
                .HasValue<Assignments>("Assignment");

            // --- Prevent cascade deletes where necessary ---

            // Announcements → Teacher
            modelBuilder.Entity<Announcements>()
                .HasOne(a => a.Teacher)
                .WithMany()
                .HasForeignKey(a => a.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Announcements → Course
            modelBuilder.Entity<Announcements>()
                .HasOne(a => a.Course)
                .WithMany(c => c.Announcements)
                .HasForeignKey(a => a.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Enrollments → Student
            modelBuilder.Entity<Enrollments>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Enrollments → Course
            modelBuilder.Entity<Enrollments>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Submissions → Student
            modelBuilder.Entity<Submissions>()
                .HasOne(s => s.Student)
                .WithMany()
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Submissions → Assignment
            modelBuilder.Entity<Submissions>()
                .HasOne(s => s.Assignment)
                .WithMany(a => a.Submissions)
                .HasForeignKey(s => s.AssignmentID)
                .OnDelete(DeleteBehavior.Restrict);

            // Attachments → Announcement
            modelBuilder.Entity<Attachments>()
                .HasOne(a => a.Announcement)
                .WithMany(a => a.Attachments)
                .HasForeignKey(a => a.AnnouncementId)
                .OnDelete(DeleteBehavior.Cascade); // likely safe
        }

    }
}
