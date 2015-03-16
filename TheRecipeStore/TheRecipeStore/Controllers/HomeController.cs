using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheRecipeStore.Models;
using TheRecipeStore.Controllers.Helpers;
using System.Data.SqlClient;


namespace TheRecipeStore.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public ActionResult Create()
        {
            Recipe recipe = new Recipe();
            recipe.Ingredients.Add(new Ingredient());
            return View(recipe);
        }

        [HttpPost]
        public ActionResult Create(Recipe model) 
        {
            if (model != null)
            {
                BOL.SubmitRecipeBOL(model);
                return View(model);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Add(Recipe model)
        {
            try
            {
                model.Ingredients.Add(new Ingredient());
                return View("Create", model);
            }
            catch
            {
                return View("Create");
            }
        }

        public ActionResult Index()
        {
            List<Recipe> recipeList = new List<Recipe>();
            recipeList = BOL.GetRecipeListBOL();

            return View(recipeList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}