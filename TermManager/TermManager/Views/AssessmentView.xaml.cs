using System;
using System.Linq;
using Xamarin.Forms;
using TermManager.Models;
using TermManager.ViewModels;
using System.Threading.Tasks;

namespace TermManager.Views
{
    public partial class AssessmentView : ContentPage
    {
        protected Assessment ass = new Assessment();

        public AssessmentView(Assessment assessment)
        {
            InitializeComponent();
            ass = assessment; 
            BindingContext = new AssessmentViewModel(Navigation, ass);
        }
    }
}
