using System;
using SQLite;

namespace TermManager.Models
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int CourseId { get; set; }
        public int TermId { get; set; }
        public string CourseTitle { get; set; }
        public DateTime StartCourseDate { get; set; }
        public DateTime EndCourseDate { get; set; }
        public string CourseStatus { get; set; }
        public string ProfessorName { get; set; }
        public string ProfessorEmail { get; set; }
        public string ProfessorPhone { get; set; }
        public string CourseNotes { get; set; }
        public bool DueDateAlert { get; set; }
        public bool StartDateAlert { get; set; }
    }
}
