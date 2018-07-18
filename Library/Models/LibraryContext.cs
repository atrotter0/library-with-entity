using Microsoft.EntityFrameworkCore;

namespace Library
{
    public class Library : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Patron> Patrons { get; set; }
        public DbSet<Copy> Copies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(@"Server=localhost;Port=8889;database=university_registrar;uid=root;pwd=root;");
        }
    }
}
