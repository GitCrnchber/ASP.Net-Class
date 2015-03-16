using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheRecipeStore.Controllers.Helpers
{
    public class BOL
    {
        internal static List<Models.Recipe> GetRecipeListBOL()
        {
            return DAL.GetRecipeListDAL();

        }

        internal static void SubmitRecipeBOL(TheRecipeStore.Models.Recipe model)
        {
            DAL.SubmitRecipeDAL(model);
        }
    }
}