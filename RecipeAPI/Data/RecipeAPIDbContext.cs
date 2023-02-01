using Microsoft.EntityFrameworkCore;
using RecipeAPI.Models;

namespace RecipeAPI.Data
{
    public class RecipeAPIDbContext : DbContext
    {

        public RecipeAPIDbContext(DbContextOptions options) : base(options)
        {



        }

        public DbSet<Recipe> Recipes {get; set;}

    }
}
