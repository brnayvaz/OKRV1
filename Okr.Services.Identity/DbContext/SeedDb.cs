using Okr.Entities;

namespace Okr.Services.Identity.DbContext
{
    public class SeedDb
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<UserDbContext>(); // dbcontexi kullanmak için startupcs de provideri parametre olraak gönderiyorum..
            context.Database.EnsureCreated();// Db create edildimi 
            // Look for any board games.
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        id = 1,
                        name = "Burhan",
                        password = "123456",
                        role = "Admin"
                    },
                    new User
                    {
                        id = 2,
                        name = "Arife",
                        password = "123456",
                        role = "Read"
                    },
                    new User { 
                        id = 3, 
                        name = "Ali Osman", 
                        password = "123456", 
                        role = "Write" 
                    });

                context.SaveChanges();
            }

        }
    }
}
