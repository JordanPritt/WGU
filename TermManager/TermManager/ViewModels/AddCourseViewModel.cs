using System;
using Xamarin.Forms;
using TermManager.Models;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace TermManager.ViewModels
{
    public class AddCourseViewModel : INotifyPropertyChanged
    {
        private readonly INavigation Nav;

        public int _courseId;
        public int _termId;
        public string _courseTitle;
        public DateTime _startDate;
        public DateTime _endDate;
        public string _courseStatus;
        public string _profName;
        public string _profEmail;
        public string _profPhone;

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
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime EndCourseDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
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
            get => _profName;
            set
            {
                _profName = value;
                OnPropertyChanged();
            }
        }
        public string ProfessorEmail
        {
            get => _profEmail;
            set
            {
                _profEmail = value;
                OnPropertyChanged();
            }
        }
        public string ProfessorPhone
        {
            get => _profPhone;
            set
            {
                _profPhone = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

        public Command SaveCommand { get; private set; }
        public Command CancelCommand { get; private set; }

        public AddCourseViewModel(INavigation navigation)
        {
            Nav = navigation;

            // set defaults
            _startDate = DateTime.Now;
            _endDate = DateTime.Now.AddDays(7);

            SaveCommand = new Command(async () => await SaveCourse());
            CancelCommand = new Command(async () => await CancelClicked());
        }

        public async Task SaveCourse()
        {
            var course = new Course
            {
                CourseStatus = _courseStatus,
                CourseTitle = _courseTitle,
                StartCourseDate = _startDate,
                EndCourseDate = _endDate,
                ProfessorEmail = _profEmail,
                ProfessorName = _profName,
                ProfessorPhone = _profPhone,
                TermId = _termId
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
                await Nav.PopAsync();
            }
        }

        public async Task CancelClicked()
        {
            await Nav.PopAsync();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
