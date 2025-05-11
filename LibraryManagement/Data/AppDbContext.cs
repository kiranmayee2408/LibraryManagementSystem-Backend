using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;


namespace LibraryManagement.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Member> Members => Set<Member>();
        public DbSet<IssuedBook> IssuedBooks => Set<IssuedBook>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add custom relationships if needed
            // Example: modelBuilder.Entity<IssuedBook>().HasOne(...)
        }
    }
}
