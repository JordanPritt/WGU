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
    public class TermViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Command SelectedCommand { get; private set; }

        protected ObservableCollection<Term> _terms;
        protected INavigation Nav { get; set; }

        public ObservableCollection<Term> AllTerms
        {
            get => _terms;
            set
            {
                _terms = value;
                OnPropertyChanged();
            }
        }

        public TermViewModel(INavigation navigation)
        {
            SelectedCommand = new Command(async () => await ItemSelected());

            Nav = navigation;
            _ = SetupPage();
        }

        public async Task SetupPage()
        {
            AllTerms = new ObservableCollection<Term>(await App.Database.GetTermsAsync());
        }

        // need to capture selected item data
        protected async Task ItemSelected()
        {
            await Nav.PushAsync(new TermDetailView());
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
