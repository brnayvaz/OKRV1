
using Microsoft.EntityFrameworkCore;
using Okr.Entities;

namespace Okr.Services.Identity.DbContext
{
    public class UserDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
           : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception e)
            {
                return 0;
            }

        }
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {

            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            return result;
        }

    }
}
