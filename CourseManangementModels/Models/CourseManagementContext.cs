using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace CourseManangementModels.Models
{
    public partial class CourseManagementContext : DbContext
    {
        public CourseManagementContext()
        {
        }

        public CourseManagementContext(DbContextOptions<CourseManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attendance> Attendances { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Major> Majors { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Semester> Semesters { get; set; } = null!;
        public virtual DbSet<Session> Sessions { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<StudentInCourse> StudentInCourses { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<SubjectTeach> SubjectTeaches { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<UserBasic> UserBasics { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Server=LAPTOP-OPG0RG4J\\PHILONG;Database=CourseManagement; User ID=sa; Password=Lenny@@2904; TrustServerCertificate=true");
                optionsBuilder.UseSqlServer(GetConnection());
            }
        }

        private String GetConnection()
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            return configuration.GetConnectionString("CourseManagement");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.SessionId });

                entity.ToTable("Attendance");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attendanc__Sessi__5070F446");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attendanc__Stude__4F7CD00D");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SemesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Course__Semester__3A81B327");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Course__SubjectI__3B75D760");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK__Course__TeacherI__398D8EEE");
            });

            modelBuilder.Entity<Major>(entity =>
            {
                entity.ToTable("Major");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasMany(d => d.Subjects)
                    .WithMany(p => p.Majors)
                    .UsingEntity<Dictionary<string, object>>(
                        "SubjectInMajor",
                        l => l.HasOne<Subject>().WithMany().HasForeignKey("SubjectId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__SubjectIn__Subje__36B12243"),
                        r => r.HasOne<Major>().WithMany().HasForeignKey("MajorId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__SubjectIn__Major__35BCFE0A"),
                        j =>
                        {
                            j.HasKey("MajorId", "SubjectId");

                            j.ToTable("SubjectInMajor");
                        });
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.RoomNo).HasMaxLength(255);
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.ToTable("Semester");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("Session");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Slot).HasMaxLength(255);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__CourseI__4222D4EF");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__RoomId__440B1D61");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK__Session__Teacher__4316F928");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.HasIndex(e => e.UserBasicId, "UQ__Student__28EF2EBB359D8ABB")
                    .IsUnique();

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.GraduationDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(255);

                entity.Property(e => e.StudentName).HasMaxLength(255);

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.MajorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Student__MajorId__47DBAE45");

                entity.HasOne(d => d.UserBasic)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.UserBasicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Student__UserBas__48CFD27E");
            });

            modelBuilder.Entity<StudentInCourse>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.CourseId });

                entity.ToTable("StudentInCourse");

                entity.Property(e => e.Gpa).HasColumnName("GPA");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentInCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StudentIn__Cours__4CA06362");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentInCourses)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StudentIn__Stude__4BAC3F29");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasMany(d => d.Prerequisites)
                    .WithMany(p => p.Subjects)
                    .UsingEntity<Dictionary<string, object>>(
                        "SubjectPrerequisite",
                        l => l.HasOne<Subject>().WithMany().HasForeignKey("PrerequisiteId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__SubjectPr__Prere__5FB337D6"),
                        r => r.HasOne<Subject>().WithMany().HasForeignKey("SubjectId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__SubjectPr__Subje__5EBF139D"),
                        j =>
                        {
                            j.HasKey("SubjectId", "PrerequisiteId").HasName("PK_CoursePrerequisite");

                            j.ToTable("SubjectPrerequisite");
                        });

                entity.HasMany(d => d.Subjects)
                    .WithMany(p => p.Prerequisites)
                    .UsingEntity<Dictionary<string, object>>(
                        "SubjectPrerequisite",
                        l => l.HasOne<Subject>().WithMany().HasForeignKey("SubjectId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__SubjectPr__Subje__5EBF139D"),
                        r => r.HasOne<Subject>().WithMany().HasForeignKey("PrerequisiteId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__SubjectPr__Prere__5FB337D6"),
                        j =>
                        {
                            j.HasKey("SubjectId", "PrerequisiteId").HasName("PK_CoursePrerequisite");

                            j.ToTable("SubjectPrerequisite");
                        });
            });

            modelBuilder.Entity<SubjectTeach>(entity =>
            {
                entity.HasKey(e => new { e.TeacherId, e.SubjectId });

                entity.ToTable("SubjectTeach");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SubjectTeaches)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SubjectTe__Subje__32E0915F");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.SubjectTeaches)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SubjectTe__Teach__31EC6D26");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.HasIndex(e => e.UserBasicId, "UQ__Teacher__28EF2EBB7D8014AF")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(225);

                entity.Property(e => e.Phone).HasMaxLength(12);

                entity.HasOne(d => d.UserBasic)
                    .WithOne(p => p.Teacher)
                    .HasForeignKey<Teacher>(d => d.UserBasicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Teacher__UserBas__2B3F6F97");
            });

            modelBuilder.Entity<UserBasic>(entity =>
            {
                entity.ToTable("UserBasic");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
