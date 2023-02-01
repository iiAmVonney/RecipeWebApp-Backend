using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeAPI.Data;
using RecipeAPI.Models;
using System.Formats.Asn1;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

namespace RecipeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : Controller
    {

        private readonly RecipeAPIDbContext dbContext;

        public RecipeController(RecipeAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            Console.WriteLine("im triggered!");
            return Ok(await dbContext.Recipes.ToListAsync());
        }

        [HttpGet("CSV")]
        public async Task<IActionResult> GetCsv()
        {
            
            var list = await dbContext.Recipes.ToListAsync();
           
            Console.WriteLine(list);

            using (var writer = new StreamWriter("./CSV/file.csv"))
            using (var csv = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(list);
            }


            var bytes = await System.IO.File.ReadAllBytesAsync("./CSV/file.csv");
            var contentType = "csv/text";
            var fileName = "Recipe.csv";
            return File(bytes, contentType, fileName);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipes(AddRecipe r)
        {
            var recipe = new Recipe()
            {
                Id = Guid.NewGuid(),
                Name = r.Name,
                Ingredients = r.Ingredients,
                Steps = r.Steps,
            };

            await dbContext.Recipes.AddAsync(recipe);
            await dbContext.SaveChangesAsync();
            /*"recipe added with Id: " +*/
            return Ok(recipe.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipes(Guid id, Recipe r)
        {
            var recipe = await dbContext.Recipes.FindAsync(id);

            if(recipe != null)
            {
                recipe.Ingredients = r.Ingredients;
                recipe.Steps = r.Steps;
            }

            
            await dbContext.SaveChangesAsync();
            /*"recipe added with Id: " +*/
            return Ok(recipe.Id);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> RemoveRecipe([FromRoute] Guid id)
        {
            var Recipe = await dbContext.Recipes.FindAsync(id);

            if(Recipe != null)
            {
                dbContext.Remove(Recipe);
                await dbContext.SaveChangesAsync();

                return Ok(Recipe.Id);

            }

            return NotFound();
        }
    }
}
