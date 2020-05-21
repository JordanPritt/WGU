using SQLite;
using TermManager.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TermManager.Data
{
    public class TermDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public TermDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Course>().Wait();
            _database.CreateTableAsync<Assessment>().Wait();
            _database.CreateTableAsync<Term>().Wait();
        }

        #region Course Table
        public Task<List<Course>> GetCoursesAsync(int termId)
        {
            return _database.Table<Course>()
                            .Where(x => x.TermId == termId)
                            .ToListAsync();
        }

        public Task<Course> GetCourseAsync(int id)
        {
            return _database.Table<Course>()
                            .Where(i => i.CourseId == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveCourseAsync(Course course)
        {
            if (course.CourseId != 0)
            {
                return _database.UpdateAsync(course);
            }
            else
            {
                return _database.InsertAsync(course);
            }
        }

        public Task<int> DeleteCourseAsync(Course course)
        {
            return _database.DeleteAsync(course);
        }
        #endregion

        #region Term Table
        public Task<List<Term>> GetTermsAsync()
        {
            return _database.Table<Term>().ToListAsync();
        }

        public Task<Term> GetTermAsync(int id)
        {
            return _database.Table<Term>()
                            .Where(i => i.TermId == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveTermAsync(Term term)
        {
            if (term.TermId != 0)
            {
                return _database.UpdateAsync(term);
            }
            else
            {
                return _database.InsertAsync(term);
            }
        }

        public Task<int> DeleteTermAsync(Term term)
        {
            return _database.DeleteAsync(term);
        }
        #endregion

        #region Assessment Table
        public Task<List<Assessment>> GetAssessmentsAsync()
        {
            return _database.Table<Assessment>().ToListAsync();
        }

        public Task<List<Assessment>> GetAssessmentsAsync(int id)
        {
            return _database.Table<Assessment>()
                            .Where(i => i.CourseId == id)
                            .ToListAsync();
        }

        public Task<Assessment> GetAssessmentByIdAsync(int num, int courseId)
        {
            return _database.Table<Assessment>()
                            .Where(i => i.AssessmentOneOrTwo == num && i.CourseId == courseId)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveAssessmentAsync(Assessment assessment)
        {
            if (assessment.AssessmentId != 0)
            {
                return _database.UpdateAsync(assessment);
            }
            else
            {
                return _database.InsertAsync(assessment);
            }
        }

        public Task<int> DeleteAssessmentAsync(Assessment assessment)
        {
            return _database.DeleteAsync(assessment);
        }
        #endregion
    }
}
