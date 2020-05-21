using System;
using System.Linq;
using Xamarin.Forms;
using TermManager.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TermManager.ViewModels
{
    public class AssessmentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Command SetAssessmentCommand { get; private set; }
        public Command DeleteAssessmentCommand { get; private set; }

        protected string _type;
        protected string _name;
        protected int _courseId;
        protected int _oneOrTwo;
        protected INavigation Nav;
        protected int _assessmentId;
        protected DateTime _dueDate;
        protected Assessment _passedInAssessment;

        public int AssessmentId
        {
            get => _assessmentId;
            set
            {
                _assessmentId = value;
                OnPropertyChanged();
            }
        }
        public int CourseId
        {
            get => _courseId;
            set
            {
                _courseId = value;
                OnPropertyChanged();
            }
        }
        public string AssessmentType
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }
        public string AssessmentName
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public DateTime AssessmentDueDate
        {
            get => _dueDate;
            set
            {
                _dueDate = value;
                OnPropertyChanged();
            }
        }
        public int AssessmentOneOrTwo
        {
            get => _oneOrTwo;
            set
            {
                _oneOrTwo = value;
                OnPropertyChanged();
            }
        }
        public Assessment Assessment
        {
            get => _passedInAssessment;
            set
            {
                _passedInAssessment = value;
                OnPropertyChanged();
            }
        }

        public AssessmentViewModel(INavigation nav, Assessment ass)
        {
            Nav = nav;

            if (ass != null)
            {
                _oneOrTwo = ass.AssessmentOneOrTwo;
                _assessmentId = ass.AssessmentId;
                _courseId = ass.CourseId;
                _dueDate = ass.DueDate;
                _name = ass.Name;
                _type = ass.Type;
            }

            SetAssessmentCommand = new Command(async () => await SetAssessment());
            DeleteAssessmentCommand = new Command(async () => await DeleteAssessment());
        }

        private async Task DeleteAssessment()
        {
            List<Assessment> assessments = await App.Database.GetAssessmentsAsync(_courseId);
            Assessment assessment = assessments.FirstOrDefault(a => a.Name == _name);

            await App.Database.DeleteAssessmentAsync(assessment);
            _ = await Nav.PopAsync();
        }

        protected async Task SetAssessment()
        {
            Assessment assessment = new Assessment
            {
                CourseId = _courseId,
                DueDate = _dueDate,
                Name = _name,
                Type = _type
            };

            if (string.IsNullOrWhiteSpace(assessment.Name) || string.IsNullOrWhiteSpace(assessment.Type))
            {
                _ = Application.Current.MainPage.DisplayAlert("Warning", "Please provide a name and/or type for the course.", "Ok");
            }
            else
            {
                await App.Database.SaveAssessmentAsync(assessment);
                _ = await Nav.PopAsync();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
