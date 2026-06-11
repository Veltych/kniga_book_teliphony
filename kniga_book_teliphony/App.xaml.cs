using kniga_book_teliphony.Services;
using kniga_book_teliphony.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
namespace kniga_book_teliphony
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            //base.OnStartup(e);
            //// 1. Создаём коллекцию сервисов
            //var services = new ServiceCollection();
            //services.AddDbContext<ApplicationContext>(options =>
            //    options.UseSqlServer("Data Source=DBSRV\\ROG2025;Initial Catalog=PhoneBookDB;Integrated Security=True;Trust Server Certificate=True"));
            //// 2. Регистрируем сервисы (Lifetime)
            //// DialogService — Singleton, так как он не хранит
            //// состояние пользователя.
            //services.AddSingleton<IDialogService, DialogService>();
            //// 3. ViewModel — Transient (при навигации нам будут
            //// нужны новые экземпляры)
            //services.AddTransient<MainViewModel>();
            //// 4. Главное окно — Singleton с явной передачей
            //// DataContext через лямбда-выражение
            //services.AddSingleton<MainWindow>(sp =>
            //{
            //    var window = new MainWindow();
            //    window.DataContext =
            //    sp.GetRequiredService<MainViewModel>();
            //    return window;
            //});
            //// 5. Создаём контейнер (ServiceProvider)
            //var serviceProvider =
            //services.BuildServiceProvider();
            //// 6. Получаем главное окно и запускаем его
            //var mainWindow =
            //serviceProvider.GetRequiredService<MainWindow>();
            //mainWindow.Show();
            base.OnStartup(e);
            var services = new ServiceCollection();
            services.AddDbContext<PhoneBookDbContext>(options =>
                options.UseSqlServer("Data Source=DBSRV\\ROG2025;Initial Catalog=PhoneBookDB;Integrated Security=True;Trust Server Certificate=True"));
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IDialogService, DialogService>();
            services.AddTransient<ContactsListViewModel>();
            services.AddTransient<AboutViewModel>();
            services.AddTransient<ContactEditViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>(sp => {
                var window = new MainWindow();
                window.DataContext = sp.GetRequiredService<MainViewModel>();
                return window;
            });
            var sp = services.BuildServiceProvider();
            sp.GetRequiredService<MainWindow>().Show();

        }
    }
}
