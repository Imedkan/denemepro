using SQLite;

namespace denemepro.Models;

public class User
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Unique]
    public string Email { get; set; } = "";

    public string Password { get; set; } = "";
}
