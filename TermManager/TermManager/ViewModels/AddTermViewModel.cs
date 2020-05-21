using System;
using Xamarin.Forms;
using TermManager.Models;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace TermManager.ViewModels
{
    public class AddTermViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Command CancelCommand { get; private set; }
        public Command SaveCommand { get; private set; }

        private INavigation Nav { get; set; }

        protected string _termTitle;
        protected DateTime _termStartDate;
        protected DateTime _termEndDate;

        public string TermTitle
        {
            get => _termTitle;
            set
            {
                _termTitle = value;
                OnPropertyChanged();
            }
        }
        public DateTime TermStartDate
        {
            get => _termStartDate;

            set
            {
                _termStartDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime TermEndDate
        {
            get => _termEndDate;
            set
            {
                _termEndDate = value;
                OnPropertyChanged();
            }
        }

        public AddTermViewModel(INavigation navigation)
        {
            Nav = navigation;

            // default dates
            _termStartDate = DateTime.Now;
            _termEndDate = DateTime.Now.AddDays(14);

            SaveCommand = new Command(async () => await SaveNewTerm());
            CancelCommand = new Command(async () => await CancelClicked());
        }

        protected async Task SaveNewTerm()
        {
            var term = new Term
            {
                TermTitle = _termTitle,
                TermStartDate = _termStartDate,
                TermEndDate = _termEndDate
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
                await Nav.PopAsync();
            }
        }

        protected async Task CancelClicked()
        {
            await Nav.PopAsync();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
