using kniga_book_teliphony.Models;
using kniga_book_teliphony.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace kniga_book_teliphony.ViewModels
{
    public class MainViewModel 
    {
        private readonly INavigationService _navigation;

        public INavigationService NavigationService => _navigation;

        public MainViewModel(INavigationService navigation)
        {
            _navigation = navigation;
            ShowContactsCommand = new RelayCommand(() => _navigation.NavigateTo<ContactsListViewModel>());
            ShowAboutCommand = new RelayCommand(() => _navigation.NavigateTo<AboutViewModel>());
            _navigation.NavigateTo<ContactsListViewModel>();
        }

        public ICommand ShowContactsCommand { get; }
        public ICommand ShowAboutCommand { get; }
    }
}
