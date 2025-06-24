using Microsoft.EntityFrameworkCore;
using FirstApp.Models;
using System.Collections.Generic;

namespace FirstApp.Data
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        //connection string is defined in OnConfiguring method
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=school.db");
        }
        //seeding data is done in OnModelCreating method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Seed students
    modelBuilder.Entity<Student>().HasData(
        new Student { StudentId = 1, Name = "Aarav" },
        new Student { StudentId = 2, Name = "Meera" }
    );

    // Seed courses
    modelBuilder.Entity<Course>().HasData(
        new Course { CourseId = 101, Title = "Mathematics" },
        new Course { CourseId = 102, Title = "Science" }
    );

    // Configure many-to-many relationship and seed join table
    modelBuilder.Entity<Student>()
        .HasMany(s => s.Courses)
        .WithMany(c => c.Students)
        .UsingEntity<Dictionary<string, object>>(
            "StudentCourses",
            j => j.HasData(
                new { StudentsStudentId = 1, CoursesCourseId = 101 },
                new { StudentsStudentId = 1, CoursesCourseId = 102 },
                new { StudentsStudentId = 2, CoursesCourseId = 101 }
            )
        );
}
        // This method is used to configure the model and seed data
        // It is called when the database is created or updated
    }
}