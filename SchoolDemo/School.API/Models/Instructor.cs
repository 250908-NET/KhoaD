using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace School.Models;


public class Instructor
{
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string FirstName { get; set; }
    [Required, MaxLength(50)]
    public string LastName { get; set; }
    public List<Course> Courses { get; set; }
}