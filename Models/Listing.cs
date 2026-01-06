using SQLite;

namespace denemepro.Models;

public class Listing
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Title { get; set; } = "";
    public string Description { get; set; } = "";

    public string City { get; set; } = "";
    public string District { get; set; } = "";

    public int Rent { get; set; }
    public int CampusMinutes { get; set; }

    public bool Furnished { get; set; }
    public string Category { get; set; } = "Room";

    public string ImageFile { get; set; } = "room1.jpg";

    public int OwnerUserId { get; set; }
}
