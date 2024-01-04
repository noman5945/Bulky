using Bulky.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options) 
        {
            
        }

        public DbSet<Category> Categories { get; set; }

        //Add Data Seeds that is initial data of the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                    new Category { CategoryId = 1, CategoryName = "Action", DisplayOrder = 3},
                    new Category { CategoryId = 2, CategoryName = "Sci-Fi", DisplayOrder = 2 },
                    new Category { CategoryId = 3, CategoryName = "History", DisplayOrder = 1 }
                );
        }

    }
}
