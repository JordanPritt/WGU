using System;
using Xamarin.Forms;
using TermManager.Models;
using TermManager.ViewModels;

namespace TermManager.Views
{
    public partial class AddTermView : ContentPage
    {
        public AddTermView()
        {
            InitializeComponent();
            BindingContext = new AddTermViewModel(Navigation);
        }
    }
}
