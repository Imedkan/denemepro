namespace denemepro.Pages;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void OnCreateClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text ?? "";
        var phone = PhoneEntry.Text ?? "";
        var p1 = PasswordEntry.Text ?? "";
        var p2 = Password2Entry.Text ?? "";

        if (p1 != p2)
        {
            await DisplayAlert("Error", "Passwords do not match.", "OK");
            return;
        }

        var result = await App.Db.RegisterAsync(email, p1, phone);
        if (!result.Ok)
        {
            await DisplayAlert("Error", result.Error, "OK");
            return;
        }

        await DisplayAlert("OK", "Account created. You can login now.", "OK");
        await Navigation.PopAsync();
    }
}