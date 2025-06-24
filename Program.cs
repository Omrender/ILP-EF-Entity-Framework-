using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FirstApp.Data;
using FirstApp.Models;

namespace FirstApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new SchoolContext();

            var students = context.Students.Include(s => s.Courses).ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"Student: {student.Name}");
                foreach (var course in student.Courses)
                    Console.WriteLine($"  Enrolled in: {course.Title}");
            }
        }
    }
}

//dotnet tool install --global dotnet-sqlite
//dotnet tool install --global dotnet-ef
//dotnet ef migrations add InitialCreate
//dotnet ef database update