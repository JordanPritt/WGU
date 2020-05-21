using System;
using Xamarin.Forms;
using TermManager.Models;
using TermManager.ViewModels;

namespace TermManager.Views
{
    public partial class TermView : ContentPage
    {
        public TermView()
        {
            InitializeComponent();
            BindingContext = new TermViewModel(Navigation);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = BindingContext as TermViewModel;
            await vm.SetupPage();
        }

        protected async void OnAddTerm(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AddTermView());
        }

        protected async void OnViewCourse(object sender, SelectedItemChangedEventArgs args)
        {
            //var item = args.SelectedItem as TermViewModel;
            var item = (Term)args.SelectedItem;
            await Navigation.PushAsync(new TermDetailView(item));
        }
    }
}