using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TeacherAssistant.Data;
using TeacherAssistant.Models.Students;
using Microsoft.EntityFrameworkCore;

namespace TeacherAssistant.Services
{
    /// <summary>
    /// Service class for handling Student logic.
    /// </summary>
    public class StudentsService
    {
        private readonly ApplicationDbContext _context;

        public StudentsService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all the students associated with a teacher.
        /// </summary>
        /// <param name="teacherId">The id of the teacher.</param>
        /// <returns>A list of the students.</returns>
        public List<Student> GetStudents(string teacherId)
        {
            List<Student> students = (from student in _context.Students
                                      where student.TeacherId == teacherId
                                      select student).ToList();
            return students;
        }

        /// <summary>
        /// Generates the table items for the a students table.
        /// </summary>
        /// <param name="teacherId">Id of the associated teacher.</param>
        /// <returns>List fof table items for students table.</returns>
        public List<StudentTableItem> GetStudentTable(string teacherId)
        {
            List<StudentTableItem> studentsTable = new List<StudentTableItem>();
            List<Student> students = GetStudents(teacherId);

            foreach (var student in students)
            {
                // TODO: implement attendance and grades
                var studentItem = new StudentTableItem()
                {
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Attendance = 0,
                    Grade = 0
                };

                studentsTable.Add(studentItem);
            }

            return studentsTable;
        }

        /// <summary>
        /// Adds a student to the database.
        /// </summary>
        /// <param name="student">The student object.</param>
        /// <returns>True or false depending on if the student saved.</returns>
        public async Task<bool> CreateStudent(Student student)
        {
            try
            {
                _context.Students.Add((Student)student);
                int result = await _context.SaveChangesAsync();

                if (result == 1)
                    return true;
                else
                    throw new DbUpdateException();
            }
            catch (DbUpdateException ex)
            {
                // TODO: log errors
                return false;
            }
            catch(Exception ex)
            {
                //TODO: log errors
                return false;
            }
        }
    }
}
