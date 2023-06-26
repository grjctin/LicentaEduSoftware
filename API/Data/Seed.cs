using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if(await context.Users.AnyAsync()) return; // daca avem useri in db, return

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");   

            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            foreach (var user in users)
            {
                //generam parole pt fiecare user pt a-l putea adauga in baza de date
                //ca in metoda register din account controller
                using var hmac = new HMACSHA512();
                user.Username = user.Username.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user); //nu se adauga efectiv in baza de date
            }

            await context.SaveChangesAsync();
        }
    }
}