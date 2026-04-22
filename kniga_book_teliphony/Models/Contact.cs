using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kniga_book_teliphony.ViewModels;

namespace kniga_book_teliphony.Models
{
    public class Contact : ObservableObject
    {
        private string _name = string.Empty;
        private string _phone = string.Empty;

        // Допишите конструктор, который вызовет метод Validate для проверки значений.
        // Если некорректные – выбросить исключение
        public string Name
        {
            get => _name;
            set
            {
                // TODO: допишите установку _name и вызов
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
                // события изменения свойства

            }
        }
        public string Phone
        {
            get => _phone;
            set
            {
                // TODO: допишите установку _phone и вызов
                if (_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged();
                }
                // события изменения свойства
            }
        }
        // TODO: добавьте метод Validate(), который
        // проверяет, что Name не пуст и Phone
        // соответствует формату +7XXXXXXXXXX (или без кода страны)
        // Метод должен возвращать bool
        public bool Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }

            if (string.IsNullOrEmpty(Phone))
            {
                return false;
            }

            if (Phone.Length == 12 && Phone.StartsWith("+7"))
            {
                for (int i = 2; i < Phone.Length; i++)
                {
                    if (!char.IsDigit(Phone[i]))
                        return false;
                }
                return true;
            }

            if (Phone.Length == 10)
            {
                foreach (char c in Phone)
                {
                    if (!char.IsDigit(c))
                        return false;
                }
                return true;
            }
            return false;
        }
    }
}
