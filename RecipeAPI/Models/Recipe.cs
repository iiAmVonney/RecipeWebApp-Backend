namespace RecipeAPI.Models
{
    public class Recipe
    {
        public Recipe()
        {
        }

        public Recipe(Guid id, string name, string ingredients, string steps)
        {
            Id = id;
            Name = name;
            Ingredients = ingredients;
            Steps = steps;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Ingredients { get; set; }

        public string Steps{ get; set; }
    }
}
