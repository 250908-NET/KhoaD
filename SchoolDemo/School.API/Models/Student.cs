using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace School.Models;

public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    [Column("FirstName")]
    public string FirstName { get; set; }
    [Required, MaxLength(50)]
    public string LastName { get; set; }
    [Required, MaxLength(50)]
    public string Email { get; set; }
    public List<Course> Courses { get; set; }
}