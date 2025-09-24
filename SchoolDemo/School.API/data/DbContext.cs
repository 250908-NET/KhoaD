using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using School.Data;
using School.Models;

namespace School.Data;

public class SchoolDbContext : DbContext
{
    // Fields
    public DbSet<Student> Students { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Course> Courses { get; set;}
}