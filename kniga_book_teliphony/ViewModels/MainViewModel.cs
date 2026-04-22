using kniga_book_teliphony.Models;
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
        public MainViewModel()
        {
            Contacts = new ObservableCollection<Contact>();
            AddCommand = new RelayCommand(
            AddContact,() => CanAddContact());
            DeleteCommand = new RelayCommand(
            DeleteContact,() => CanDeleteContact());
        }
        private void AddContact()
        {
            Contact newContact = new Contact
            {
                Name = Name,
                Phone = Phone,
            };
            Contacts.Add(newContact);
            Name = string.Empty;
            Phone = string.Empty;
        }
        private bool CanAddContact()
        {
            if (Name != null) { return true; }
            if (Phone != null) { return true; }
            return false; // временная заглушка
        }
        private void DeleteContact()
        {
            if (SelectedContact != null)
            Contacts.Remove(SelectedContact);
        }
        private bool CanDeleteContact()
        {
            if (SelectedContact != null) { return true; }
            return false; // временная заглушка
        }
    }
}
