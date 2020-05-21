using System;
using SQLite;

namespace TermManager.Models
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int AssessmentId { get; set; }
        public int CourseId { get; set; }
        public int AssessmentOneOrTwo { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
    }
}