using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheRecipeStore.Models;

namespace TheRecipeStore.Controllers.Helpers
{
    public class DAL
    {
        internal static void SubmitRecipeDAL(Recipe model)
        {
            //recipeName = "Pancakes";
            //submitterID = "Anklyosaurus";
            //directions = "cook the pankcakes all day";

            //A reader to store values
            SqlDataReader sqlDataReader;

            //Connection
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["RecipeConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            {
                //Create a command to execute Stored Procedure
                SqlCommand sqlCommand = new SqlCommand("addRecipeStub", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                //Add parameters
                sqlCommand.Parameters.AddWithValue("@RecipeName", model.RecipeName);
                sqlCommand.Parameters.AddWithValue("@SubmitterID", model.SubmitterID);
                sqlCommand.Parameters.AddWithValue("@Directions", model.Directions);

                //Open connection
                conn.Open();

                //Execute Stored Procedure
                sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Close();

                sqlCommand = new SqlCommand("getRecipeID", conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@RecipeName", model.RecipeName);
                sqlCommand.Parameters.AddWithValue("@SubmitterID", model.SubmitterID);

                sqlDataReader = sqlCommand.ExecuteReader();
                int lastID = (sqlDataReader.Read()) ? sqlDataReader.GetInt32(0) : 0;
                sqlDataReader.Close();
                
                foreach (Ingredient i in model.Ingredients)
                {
                    sqlCommand = new SqlCommand("addIngredient", conn);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@RecipeID", lastID);
                    sqlCommand.Parameters.AddWithValue("@Ingredient", i.ingredientName);
                    sqlCommand.Parameters.AddWithValue("@Amount", i.amount);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    sqlDataReader.Close();
                }

                var newKeywords = model.Keyword.Split(',');
                foreach (string s in newKeywords)
                {
                    sqlCommand = new SqlCommand("addKeyword", conn);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@RecipeID", lastID);
                    sqlCommand.Parameters.AddWithValue("@Keyword", s);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    sqlDataReader.Close();
                }

                if (!sqlDataReader.IsClosed)
                {
                    sqlDataReader.Close();
                }

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }

                //return View(model);
            }
        }

        internal static List<Recipe> GetRecipeListDAL()
        {
            List<Recipe> recipeList = new List<Recipe>();

            //declare a connection
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["RecipeConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);

            //declare a command
            SqlCommand sqlCommand = new SqlCommand("select * from dbo.Recipes;", conn);

            //open the connection
            conn.Open();

            //declare a datareader
            SqlDataReader sqlDataReader;

            //load the data
            sqlDataReader = sqlCommand.ExecuteReader();

            //loop and store the data in the student list

            while (sqlDataReader.Read())
            {
                Recipe recipe = new Recipe();

                recipe.RecipeId = int.Parse(sqlDataReader["RecipeID"].ToString());
                recipe.RecipeName = sqlDataReader["RecipeName"].ToString();
                recipe.SubmitterID = sqlDataReader["SubmitterID"].ToString();
                recipe.Directions = sqlDataReader["Directions"].ToString();

                recipeList.Add(recipe);
            }

            if (!sqlDataReader.IsClosed)
            {
                sqlDataReader.Close();
            }

            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }

            return recipeList;
        }
    }
}