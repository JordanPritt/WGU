using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherAssistant.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Required]
        public int Grade { get; set; }

        [Display(Name = "Teacher ID")]
        [Required]
        public int TeacherId { get; set; }
    }
}
