using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherAssistant.Models.Students
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string TeacherId { get; set; }
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
