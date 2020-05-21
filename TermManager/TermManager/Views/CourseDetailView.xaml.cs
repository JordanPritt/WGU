using System;
using Xamarin.Forms;
using TermManager.Models;
using TermManager.ViewModels;
using System.Threading.Tasks;

namespace TermManager.Views
{
    public partial class CourseDetailView : ContentPage
    {
        protected Course Course = new Course();

        public CourseDetailView(Course course)
        {
            InitializeComponent();
            BindingContext = new CourseDetailViewModel(Navigation, course);
        }

        protected void OnViewAssessment(object sender, SelectedItemChangedEventArgs args)
        {
            var assessment = (Assessment)args.SelectedItem;

            if (assessment.DueDate < DateTime.Now)
                assessment.DueDate = DateTime.Now;

            Navigation.PushAsync(new AssessmentView(assessment));
        }

        protected async Task OnAppearing()
        {
            base.OnAppearing();

            var vm = BindingContext as CourseDetailViewModel;
            await vm.GetAssessments(Course.CourseId);
        }
    }
}
