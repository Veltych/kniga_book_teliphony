using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kniga_book_teliphony.Services
{
    public interface INavigationAware
    {
        void OnNavigatedTo(object? parameter);
    }
}
