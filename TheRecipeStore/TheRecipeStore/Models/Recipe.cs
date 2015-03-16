using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TheRecipeStore.Models
{
    public class Ingredient
    {
        public Ingredient()
        {

        }

        public Ingredient(string name, string amount)
        {
            this.ingredientName = name;
            this.amount = amount;
        }

        public string ingredientName { get; set; }
        public string amount { get; set; }
    }

    public class Recipe
    {
        public Recipe()
        {
            Ingredients = new List<Ingredient>();
        }

        //Main Recipe Info
        public int RecipeId { get; set; }
        [Required]
        public string RecipeName { get; set; }
        [Required]
        public string SubmitterID { get; set; }
        [Required]
        public string Directions { get; set; }

        //Ingredient Info
        [Required]
        public IList<Ingredient> Ingredients { get; set; }

        //Keyword Info
        [Required]
        public string Keyword { get; set; }
    }
}