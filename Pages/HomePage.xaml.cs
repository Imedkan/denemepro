using System.Collections.ObjectModel;
using denemepro.Models;

namespace denemepro.Pages;

public partial class HomePage : ContentPage
{
    private readonly ObservableCollection<Listing> _items = new ObservableCollection<Listing>();

    public HomePage()
    {
        InitializeComponent();

        CategoryPicker.ItemsSource = new List<string>
        {
            "All",
            "Room",
            "Whole Apartment",
            "Looking for Roommate"
        };
        CategoryPicker.SelectedIndex = 0;

        ListingsView.ItemsSource = _items;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadAsync();
    }

    private async Task LoadAsync()
    {
        var all = await App.Db.GetAllListingsAsync();

        var city = (CityEntry.Text ?? "").Trim();
        var cat = CategoryPicker.SelectedItem?.ToString() ?? "All";
        var furnishedOnly = FurnishedOnlySwitch.IsToggled;

        int maxRent = 0;
        int.TryParse((MaxRentEntry.Text ?? "").Trim(), out maxRent);

        int maxMin = 0;
        int.TryParse((MaxMinEntry.Text ?? "").Trim(), out maxMin);

        var filtered = all.AsEnumerable();

        if (city.Length > 0)
            filtered = filtered.Where(x => x.City.Contains(city, StringComparison.OrdinalIgnoreCase));

        if (cat != "All")
            filtered = filtered.Where(x => x.Category == cat);

        if (furnishedOnly)
            filtered = filtered.Where(x => x.Furnished);

        if (maxRent > 0)
            filtered = filtered.Where(x => x.Rent <= maxRent);

        if (maxMin > 0)
            filtered = filtered.Where(x => x.CampusMinutes <= maxMin);

        _items.Clear();
        foreach (var item in filtered)
            _items.Add(item);
    }

    private async void OnApplyFilterClicked(object sender, EventArgs e)
    {
        await LoadAsync();
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddListingPage());
    }

    private void OnLogoutClicked(object sender, EventArgs e)
    {
        App.CurrentUser = null;
        Application.Current!.MainPage = new NavigationPage(new LoginPage());
    }
}
