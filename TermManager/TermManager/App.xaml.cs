using System;
using System.IO;
using Xamarin.Forms;
using TermManager.Data;
using TermManager.Views;
using TermManager.Models;

namespace TermManager
{
    public partial class App : Application
    {
        static TermDatabase database;

        public static TermDatabase Database
        {
            get
            {
                if (database == null)
                {
                    InitDb();
                    database = new TermDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TermDB.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TermView());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected static void InitDb()
        {
            database = new TermDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TermDB.db3"));

            // seed init data
            // create term
            var term = new Term
            {
                TermStartDate = DateTime.Now,
                TermEndDate = DateTime.Now.AddDays(150),
                TermTitle = "Spring Term"
            };

            database.SaveTermAsync(term);

            // create course
            var course = new Course
            {
                TermId = 1,
                CourseId = 0,
                CourseTitle = "Software Engineering",
                StartCourseDate = DateTime.Now,
                EndCourseDate = DateTime.Now.AddDays(14),
                StartDateAlert = true,
                DueDateAlert = true,
                ProfessorName = "Jordan Pritt",
                ProfessorEmail = "jpritt1@wgu.edu",
                ProfessorPhone = "(540)-336-5725",
                CourseStatus = "in progress",
                CourseNotes = "This is the first course of the term. Hoot hoot for being a night owl!"
            };

            database.SaveCourseAsync(course);

            // create assessments
            var ass1 = new Assessment
            {
                CourseId = 1,
                DueDate = DateTime.Now.AddDays(14),
                Name = "Software Project",
                Type = "Objective",
            };
            var ass2 = new Assessment
            {
                CourseId = 1,
                DueDate = DateTime.Now.AddDays(14),
                Name = "Software Test",
                Type = "Performance",
            };

            database.SaveAssessmentAsync(ass1);
            database.SaveAssessmentAsync(ass2);
        }
    }
}
