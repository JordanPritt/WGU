using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherAssistant.Models.Students
{
    public class StudentTableItem
    {
        public int StudentId { get; set; }
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public int Attendance { get; set; }
        public decimal Grade { get; set; }
    }
}
