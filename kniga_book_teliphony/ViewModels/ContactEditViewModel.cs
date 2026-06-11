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
using System.Windows.Navigation;

namespace kniga_book_teliphony.ViewModels
{
    public class ContactEditViewModel : ObservableObject, INavigationAware
    {
        private readonly INavigationService _navigation;
        private readonly IDialogService _dialogService;
        private Contact _contact = null!;
        private readonly PhoneBookDbContext _context;
        public ObservableCollection<Contact> Contacts { get; set; }

        private string _editName = string.Empty;
        private string _editPhone = string.Empty;
        public string EditName
        {
            get => _editName;
            //set { _contact.Name = value; OnPropertyChanged(); }
            set => Set(ref _editName, value);
        }
        public string EditPhone
        {
            get => _contact.Phone;
            //set { _contact.Phone = value; OnPropertyChanged(); }
            set => Set(ref _editPhone, value);
        }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ContactEditViewModel(INavigationService navigation, IDialogService dialogService, PhoneBookDbContext context)
        {
            Contacts = new ObservableCollection<Contact>();
            _navigation = navigation;
            _dialogService = dialogService;
            //SaveCommand = new RelayCommand(
            //() => _navigation.NavigateTo<ContactsListViewModel>());
            //CancelCommand = new RelayCommand(
            //() => _navigation.NavigateTo<ContactsListViewModel>());
            SaveCommand = new RelayCommand(Save, CanSave);
            CancelCommand = new RelayCommand(Cancel);
            _context = context;
            Contacts = new ObservableCollection<Contact>(
            _context.Contacts.ToList()); //один из вариантов
        }
        public void OnNavigatedTo(object? parameter)
        {
            if (parameter is Contact c)
            {
                _contact = c;
                c.Name = EditName;
                c.Phone = EditPhone;
            }
        }

        private void Save()
        {
            _contact.Name = EditName;
            _contact.Phone = EditPhone;

            _dialogService.ShowInfo("Contact обновлен");
            _navigation.NavigateTo<ContactsListViewModel>();
        }

        private bool CanSave()
        {
            return Contact.Validate(EditName, EditPhone);
        }

        private void Cancel()
        {
            _navigation.NavigateTo<ContactsListViewModel>();
        }

        public void SaveNewContact()
        {
            try
            {
                var newContact = new Contact
                { Name = EditName, Phone = EditPhone };
                // 1. Помечаем объект как добавленный
                _context.Contacts.Add(newContact);
                // 2. Сохраняем изменения в БД (генерирует INSERT)
                _context.SaveChanges();
            }
            catch (Exception ex) { }
        }
            

        public void SaveChanges()
        {
            // Объект _contact уже отслеживается контекстом
            _contact.Name = EditName;
            _contact.Phone = EditPhone;
            // SaveChanges обнаружит изменения и сгенерирует UPDATE
            _context.SaveChanges();
            _navigation.NavigateTo<ContactsListViewModel>();
        }
    }
}
