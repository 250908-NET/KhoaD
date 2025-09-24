using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace School.Models;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public Instructor Instructor { get; set; }
    public List<Student> Students { get; set; }
}