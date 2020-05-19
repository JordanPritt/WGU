using System;

namespace TeacherAssistant.Models.Students
{
    public interface IStudent
    {
        public int StudentId { get; set; }
        public string TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
