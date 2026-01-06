using denemepro.Models;
using Microsoft.Maui;

namespace denemepro.Pages;

public partial class AddListingPage : ContentPage
{
    public AddListingPage()
    {
        InitializeComponent();

        CategoryPicker.ItemsSource = new List<string>
        {
            "Room",
            "Whole Apartment",
            "Looking for Roommate"
        };
        CategoryPicker.SelectedIndex = 0;

        ImagePicker.ItemsSource = new List<string>
        {
            "room1.jpg",
            "room2.jpg",
            "room3.jpg",
            "room4.jpg",
            "room5.jpg"
        };
        ImagePicker.SelectedIndex = 0;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (App.CurrentUser == null)
        {
            await DisplayAlert("Error", "Please login again.", "OK");
            Application.Current!.MainPage = new NavigationPage(new LoginPage());
            return;
        }

        var title = (TitleEntry.Text ?? "").Trim();
        var city = (CityEntry.Text ?? "").Trim();
        var district = (DistrictEntry.Text ?? "").Trim();

        if (title.Length == 0 || city.Length == 0 || district.Length == 0)
        {
            await DisplayAlert("Error", "Title, City, District are required.", "OK");
            return;
        }

        if (!int.TryParse((RentEntry.Text ?? "").Trim(), out var rent) || rent <= 0)
        {
            await DisplayAlert("Error", "Rent must be a valid number.", "OK");
            return;
        }

        if (!int.TryParse((MinEntry.Text ?? "").Trim(), out var minutes) || minutes < 0)
        {
            await DisplayAlert("Error", "Campus minutes must be a valid number.", "OK");
            return;
        }

        var listing = new Listing
        {
            Title = title,
            Description = (DescEntry.Text ?? "").Trim(),
            City = city,
            District = district,
            Rent = rent,
            CampusMinutes = minutes,
            Furnished = FurnishedSwitch.IsToggled,
            Category = CategoryPicker.SelectedItem?.ToString() ?? "Room",
            ImageFile = ImagePicker.SelectedItem?.ToString() ?? "room1.jpg",
            OwnerUserId = App.CurrentUser.Id
        };

        await App.Db.AddListingAsync(listing);
        await Navigation.PopAsync();
    }
}
