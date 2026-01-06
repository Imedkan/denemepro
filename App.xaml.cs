using denemepro.Data;
using denemepro.Models;
using denemepro.Pages;

namespace denemepro;

public partial class App : Application
{
    public static AppDb Db { get; } = new AppDb();
    public static User? CurrentUser { get; set; }

    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new LoginPage());
    }
}
