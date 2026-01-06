namespace denemepro.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text ?? "";
        var password = PasswordEntry.Text ?? "";

        var user = await App.Db.LoginAsync(email, password);
        if (user == null)
        {
            await DisplayAlert("Error", "Invalid email or password.", "OK");
            return;
        }

        App.CurrentUser = user;

        var home = new HomePage();
        await Navigation.PushAsync(home);
        Navigation.RemovePage(this);
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}
