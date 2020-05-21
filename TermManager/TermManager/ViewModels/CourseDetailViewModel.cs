using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using TermManager.Models;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TermManager.Views;

namespace TermManager.ViewModels
{
    public class CourseDetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Command UpdateCourseCommand { get; private set; }
        public Command AddAssessmentCommand { get; private set; }
        public Command DeleteCourseCommand { get; private set; }
        public Command ShareNoteCommand { get; private set; }
        public Command AssessmentOneCommand { get; private set; }
        public Command AssessmentTwoCommand { get; private set; }
        protected readonly INavigation Nav;

        protected int _termId;
        protected string _name;
        protected int _courseId;
        protected string _email;
        protected string _phone;
        protected bool _dueDateAlert;
        protected string _courseTitle;
        protected string _courseNotes;
        protected bool _startDateAlert;
        protected string _courseStatus;
        protected DateTime _endCourseDate;
        protected DateTime _startCourseDate;
        protected List<Assessment> _assessments;
        protected string _assessmentOne;
        protected string _assessmentTwo;

        public int CourseId
        {
            get => _courseId;
            set
            {
                _courseId = value;
                OnPropertyChanged();
            }
        }
        public int TermId
        {
            get => _termId;
            set
            {
                _termId = value;
                OnPropertyChanged();
            }
        }
        public string CourseTitle
        {
            get => _courseTitle;
            set
            {
                _courseTitle = value;
                OnPropertyChanged();
            }
        }
        public DateTime StartCourseDate
        {
            get => _startCourseDate;
            set
            {
                _startCourseDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime EndCourseDate
        {
            get => _endCourseDate;
            set
            {
                _endCourseDate = value;
                OnPropertyChanged();
            }
        }
        public string CourseStatus
        {
            get => _courseStatus;
            set
            {
                _courseStatus = value;
                OnPropertyChanged();
            }
        }
        public string ProfessorName
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string ProfessorEmail
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public string ProfessorPhone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }
        public string CourseNotes
        {
            get => _courseNotes;
            set
            {
                _courseNotes = value;
                OnPropertyChanged();
            }
        }
        public bool StartDateAlert
        {
            get => _startDateAlert;
            set
            {
                _startDateAlert = value;
                OnPropertyChanged();
            }
        }
        public bool DueDateAlert
        {
            get => _dueDateAlert;
            set
            {
                _dueDateAlert = value;
                OnPropertyChanged();
            }
        }
        public List<Assessment> Assessments
        {
            get => _assessments;
            set
            {
                _assessments = value;
                OnPropertyChanged();
            }
        }

        public CourseDetailViewModel(INavigation nav, Course course = null)
        {
            Nav = nav;

            if (course != null)
            {
                _courseId = course.CourseId;
                _termId = course.TermId;
                _courseTitle = course.CourseTitle;
                _courseStatus = course.CourseStatus;
                _startCourseDate = course.StartCourseDate;
                _endCourseDate = course.EndCourseDate;
                _name = course.ProfessorName;
                _email = course.ProfessorEmail;
                _phone = course.ProfessorPhone;
                _courseNotes = course.CourseNotes;
                _startDateAlert = course.StartDateAlert;
                _dueDateAlert = course.DueDateAlert;
            }

            _ = GetAssessments(course.CourseId);

            AddAssessmentCommand = new Command(async () => await AddAssessment());
            UpdateCourseCommand = new Command(async () => await UpdateCourse());
            DeleteCourseCommand = new Command(async () => await DeleteCourse());
            ShareNoteCommand = new Command(async () => await ShareNote());
        }

        public async Task GetAssessments(int courseId)
        {
            if (_courseId >= 0)
                courseId = CourseId;

            Assessments = await App.Database.GetAssessmentsAsync(courseId);
        }

        protected async Task AddAssessment()
        {
            Assessment ass = new Assessment();

            if (_assessments.Count >= 2)
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Sorry, you can only have two Assessments.", "Ok");
            }
            else
            {
                ass.CourseId = _courseId;
                await Nav.PushAsync(new AssessmentView(ass));
            }

        }

        protected async Task DeleteCourse()
        {
            bool okToDelete = await Application.Current.MainPage.DisplayAlert("Delete Course", "Are you sure you want to delete this course?", "Delete", "Cancel");

            if (okToDelete == true)
            {
                Course course = new Course
                {
                    CourseId = _courseId,
                    TermId = _termId,
                    CourseTitle = _courseTitle,
                    CourseStatus = _courseStatus,
                    StartCourseDate = _startCourseDate,
                    EndCourseDate = _endCourseDate,
                    ProfessorName = _name,
                    ProfessorEmail = _email,
                    ProfessorPhone = _phone,
                    CourseNotes = _courseNotes,
                    StartDateAlert = _startDateAlert,
                    DueDateAlert = _dueDateAlert
                };

                await App.Database.DeleteCourseAsync(course);
                _ = Nav.PopAsync();
            }
        }

        protected async Task UpdateCourse()
        {
            Course course = new Course
            {
                CourseId = _courseId,
                TermId = _termId,
                CourseStatus = _courseStatus,
                CourseTitle = _courseTitle,
                StartCourseDate = _startCourseDate,
                EndCourseDate = _endCourseDate,
                ProfessorName = _name,
                ProfessorEmail = _email,
                ProfessorPhone = _phone,
                CourseNotes = _courseNotes,
                StartDateAlert = _startDateAlert,
                DueDateAlert = _dueDateAlert
            };

            if (course.StartCourseDate >= course.EndCourseDate)
            {
                _ = Application.Current.MainPage.DisplayAlert("Warning", "Please select a start date before the end date.", "Ok");
            }
            else if (string.IsNullOrWhiteSpace(course.CourseStatus)
                     || string.IsNullOrWhiteSpace(course.CourseTitle)
                     || string.IsNullOrWhiteSpace(course.ProfessorEmail)
                     || string.IsNullOrWhiteSpace(course.ProfessorPhone)
                     || string.IsNullOrWhiteSpace(course.ProfessorName))
            {
                _ = Application.Current.MainPage.DisplayAlert("Warning", "Please make sure all options have a value.", "Ok");
            }
            else
            {
                await App.Database.SaveCourseAsync(course);
                _ = Nav.PopAsync();
            }
        }

        protected async Task ShareNote()
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = _courseNotes,
                Title = "Share Note"
            });
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
