using kniga_book_teliphony.Models;
using kniga_book_teliphony.Services;
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
    public class MainViewModel : ObservableObject
    {
        // Коллекция контактов
        private readonly IDialogService _dialogService;
        
        public ObservableCollection<Contact> Contacts { get; set; }
        private string _name = string.Empty;
        private string _phone = string.Empty;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        private Contact? _selectedContact;
        public Contact? SelectedContact
        {
            get => _selectedContact;
            set => Set(ref _selectedContact, value);
        }
        // Команды
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public MainViewModel(IDialogService dialogService)
        {
            Contacts = new ObservableCollection<Contact>();
            AddCommand = new RelayCommand(
            AddContact,() => CanAddContact());
            DeleteCommand = new RelayCommand(
            DeleteContact,() => CanDeleteContact());
            _dialogService = dialogService;
        }
        private void AddContact()
        {
            // Проверка на дубликат по номеру телефона
            if (Contacts.Any(c => c.Phone == _phone))
            {
                _dialogService.ShowWarning(
                "Контакт с таким номером уже существует!");
                return;
            }
            Contact newContact = new Contact
            {
                Name = Name,
                Phone = Phone,
            };
            Contacts.Add(newContact);
            _dialogService.ShowInfo($"{Name}, {Phone}");
            //Name = string.Empty;
            //Phone = string.Empty;
        }
        private bool CanAddContact()
        {
            if (Name != null) { return true; }
            if (Phone != null) { return true; }
            return false; // временная заглушка
        }
        private void DeleteContact()
        {
            if (_dialogService.ShowConfirmation("Вы уверены, что хотите удалить?", "Удаление")) { 
            if (SelectedContact != null) {
                _dialogService.ShowWarning("tochno???");
                Contacts.Remove(SelectedContact);
                } 
            }
            _dialogService.ShowInfo($"{Name}, {Phone} ты удален!!! idi nahyi!!");
        }
        private bool CanDeleteContact()
        {
            if (SelectedContact != null) { return true; }
            return false; // временная заглушка
        }
    }
}
