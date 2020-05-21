using System;
using Xamarin.Forms;
using TermManager.Models;
using TermManager.ViewModels;
using System.Collections.Generic;

namespace TermManager.Views
{
    public partial class AddCourseView : ContentPage
    {
        public AddCourseView(Term term)
        {
            InitializeComponent();
            BindingContext = new AddCourseViewModel(Navigation)
            {
                _termId = term.TermId
            };
        }
    }
}
