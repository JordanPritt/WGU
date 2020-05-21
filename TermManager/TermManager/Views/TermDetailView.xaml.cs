using System;
using Xamarin.Forms;
using TermManager.Models;
using TermManager.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TermManager.Views
{
    public partial class TermDetailView : ContentPage
    {
        protected readonly Term Term = new Term();

        public TermDetailView(Term term = null)
        {
            Term = term;

            InitializeComponent();
            BindingContext = new TermDetailViewModel(Navigation)
            {
                 TermId = term.TermId,
                 TermStartDate = term.TermStartDate,
                 TermEndDate = term.TermEndDate,
                 TermTitle = term.TermTitle
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is TermDetailViewModel vm)
                await vm.GetCourses();

        }

        protected async void OnViewCourse(object sender, SelectedItemChangedEventArgs args)
        {
            var item = (Course)args.SelectedItem;
            await Navigation.PushAsync(new CourseDetailView(item));
        }
    }
}
