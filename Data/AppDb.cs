using SQLite;
using denemepro.Models;

namespace denemepro.Data;

public class AppDb
{
    private SQLiteAsyncConnection? _db;
    private bool _initialized;

    private async Task InitAsync()
    {
        if (_initialized) return;

        var path = Path.Combine(FileSystem.AppDataDirectory, "denemepro.db3");
        _db = new SQLiteAsyncConnection(path);

        await _db.CreateTableAsync<User>();
        await _db.CreateTableAsync<Listing>();

        _initialized = true;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        await InitAsync();
        return await _db!.Table<User>().Where(x => x.Email == email).FirstOrDefaultAsync();
    }

    public async Task<(bool Ok, string Error)> RegisterAsync(string email, string password)
    {
        email = (email ?? "").Trim();
        password = (password ?? "").Trim();

        if (email.Length < 3) return (false, "Email is required.");
        if (password.Length < 3) return (false, "Password is too short.");

        await InitAsync();

        var existing = await GetUserByEmailAsync(email);
        if (existing != null) return (false, "This email is already registered.");

        var user = new User { Email = email, Password = password };
        await _db!.InsertAsync(user);
        return (true, "");
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        email = (email ?? "").Trim();
        password = (password ?? "").Trim();

        if (email.Length == 0 || password.Length == 0) return null;

        await InitAsync();
        return await _db!.Table<User>()
            .Where(x => x.Email == email && x.Password == password)
            .FirstOrDefaultAsync();
    }

    public async Task AddListingAsync(Listing listing)
    {
        await InitAsync();
        await _db!.InsertAsync(listing);
    }

    public async Task<List<Listing>> GetAllListingsAsync()
    {
        await InitAsync();
        return await _db!.Table<Listing>().OrderByDescending(x => x.Id).ToListAsync();
    }
}
