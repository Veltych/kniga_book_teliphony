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

namespace kniga_book_teliphony.ViewModels
{
    public class ContactsListViewModel : ObservableObject
    {
        // Коллекция контактов
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigation;

        private readonly PhoneBookDbContext _context;

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
        public ICommand EditCommand { get; }
        public ContactsListViewModel(IDialogService dialogService, INavigationService navigation, PhoneBookDbContext context)
        {
            _context = context;
            _dialogService = dialogService;
            _navigation = navigation;

            Contacts = new ObservableCollection<Contact>();
            AddCommand = new RelayCommand(AddContact, () => CanAddContact());
            DeleteCommand = new RelayCommand(DeleteContact, () => CanDeleteContact());
            EditCommand = new RelayCommand(EditContact, () => CanEditContact());
            
            Contacts = new ObservableCollection<Contact>(
            _context.Contacts.ToList()); //один из вариантов
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
            //Contact newContact = new Contact
            //{
            //    Name = Name,
            //    Phone = Phone,
            //};
            //Contacts.Add(newContact);
            Contacts.Add(new Contact { Name = Name, Phone = Phone});
            _dialogService.ShowInfo($"{Name}, {Phone}");
            if (Contact.Validate(Name, Phone))
            {
                Contacts.Add(new Contact { Name = Name, Phone = Phone });
                _dialogService.ShowInfo("Contact added");

                Name = string.Empty;
                Phone = string.Empty;
            }
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
            if (SelectedContact == null) return;
            try
            {
                if (_dialogService.ShowConfirmation("Вы уверены, что хотите удалить?", "Удаление"))
                {
                    if (SelectedContact != null)
                    {
                        _dialogService.ShowWarning("tochno???");
                        //Contacts.Remove(SelectedContact);
                        // 1. Помечаем объект как удалённый
                        _context.Contacts.Remove(SelectedContact);
                        // 2. Сохраняем изменения (генерирует DELETE)
                        _context.SaveChanges();
                        // 3. Обновляем UI коллекцию
                        Contacts.Remove(SelectedContact);
                    }
                }
                _dialogService.ShowInfo($"{Name}, {Phone} ты удален!!! idi nahyi!!");
            }
            catch (Exception ex)
            {
                _dialogService.ShowError($"Error when deleting {ex.Message}");
            }
        }
        private bool CanDeleteContact()
        {
            if (SelectedContact != null) { return true; }
            return false; // временная заглушка
        }

        private void EditContact()
        {
            if (SelectedContact != null)
            {
                _navigation.NavigateTo<ContactEditViewModel>(SelectedContact);
            }
        }

        private bool CanEditContact()
        {
            return SelectedContact != null;
        }
        //private void DeleteContact(Contact contact)
        //{
        //    if (contact == null) return;
        //    // 1. Помечаем объект как удалённый
        //    _context.Contacts.Remove(contact);
        //    // 2. Сохраняем изменения (генерирует DELETE)
        //    _context.SaveChanges();
        //    // 3. Обновляем UI коллекцию
        //    Contacts.Remove(contact);
        //}
    }
}
