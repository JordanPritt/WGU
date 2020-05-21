using System;
using Xamarin.Forms;
using TermManager.Views;
using TermManager.Models;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace TermManager.ViewModels
{
    public class TermDetailViewModel : INotifyPropertyChanged
    {
        protected ObservableCollection<Course> _courses;
        protected DateTime _startDate;
        protected DateTime _endDate;
        protected string _termTitle;
        protected int _termId;

        public event PropertyChangedEventHandler PropertyChanged;
        public Command AddCourseCommand { get; private set; }
        public Command UpdateTermCommand { get; private set; }
        public Command DeleteTermCommand { get; private set; }
        public INavigation Nav;

        public ObservableCollection<Course> AllCourses
        {
            get => _courses;
            set
            {
                _courses = value;
                OnPropertyChanged();
            }
        }
        public DateTime TermStartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime TermEndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }
        public int TermId
        {
            get => _termId;
            set
            {
                _termId = value;
                _ = GetCourses();
                _ = CheckForAlerts();
                OnPropertyChanged();
            }
        }
        public string TermTitle
        {
            get => _termTitle;
            set
            {
                _termTitle = value;
                OnPropertyChanged();
            }
        }

        public TermDetailViewModel(INavigation nav = null)
        {
            // set source
            Nav = nav;

            AddCourseCommand = new Command(async () => await AddCourse());
            DeleteTermCommand = new Command(async () => await DeleteTerm());
            UpdateTermCommand = new Command(async () => await UpdateTerm());
        }

        private async Task CheckForAlerts()
        {
            try
            {
                var courses = new ObservableCollection<Course>(await App.Database.GetCoursesAsync(TermId));

                foreach (Course course in courses)
                {
                    if (course.StartDateAlert == true)
                    {
                        if (course.StartCourseDate > DateTime.Today)
                            _ = Application.Current.MainPage.DisplayAlert("Alert Notification", $"{course.CourseTitle} starts on {course.StartCourseDate.ToShortDateString()}", "Ok");
                    }

                    if (course.DueDateAlert == true)
                    {
                        if (course.EndCourseDate > DateTime.Today)
                            _ = Application.Current.MainPage.DisplayAlert("Alert Notification", $"{course.CourseTitle} ends on {course.EndCourseDate.ToShortDateString()}", "Ok");
                    }
                }
            }
            catch
            {
                _ = Application.Current.MainPage.DisplayAlert("Warning", "Sorry, no courses where found in this term.", "Ok");
            }
        }

        public async Task GetCourses()
        {
            AllCourses = new ObservableCollection<Course>(await App.Database.GetCoursesAsync(TermId));
        }

        protected async Task DeleteTerm()
        {
            bool willDelete = await Application.Current.MainPage.DisplayAlert("Delete Term", "Are you sure you would like to delete this term?", "Delete", "Cancel");

            if (willDelete == true)
            {
                Term term = new Term
                {
                    TermId = _termId,
                    TermStartDate = _startDate,
                    TermEndDate = _endDate,
                    TermTitle = _termTitle
                };

                await App.Database.DeleteTermAsync(term);
                _ = Nav.PopAsync();
            }
        }

        protected async Task UpdateTerm()
        {
            Term term = new Term
            {
                TermId = _termId,
                TermStartDate = _startDate,
                TermEndDate = _endDate,
                TermTitle = _termTitle
            };

            if (term.TermStartDate >= term.TermEndDate)
            {
                _ = Application.Current.MainPage.DisplayAlert("Warning", "Please select a start date before the end date.", "Ok");
            }
            else if (string.IsNullOrWhiteSpace(term.TermTitle))
            {
                _ = Application.Current.MainPage.DisplayAlert("Warning", "Please select a start date before the end date.", "Ok");
            }
            else
            {
                await App.Database.SaveTermAsync(term);
                _ = Nav.PopAsync();
            }
        }

        protected async Task AddCourse()
        {
            Term term = await App.Database.GetTermAsync(TermId);
            await Nav.PushAsync(new AddCourseView(term));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
