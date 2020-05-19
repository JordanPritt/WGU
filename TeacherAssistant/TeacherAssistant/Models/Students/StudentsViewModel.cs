using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherAssistant.Models.Students
{
    public class StudentsViewModel : IStudent
    {
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
        public List<StudentTableItem> StudentsTable { get; set; }

        public static implicit operator Student(StudentsViewModel viewModel)
        {
            return new Student
            {
                StudentId = viewModel.StudentId,
                TeacherId = viewModel.TeacherId,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email
            };
        }
    }
}
