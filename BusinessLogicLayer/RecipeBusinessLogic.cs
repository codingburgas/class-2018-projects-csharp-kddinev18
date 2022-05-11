﻿using DataAccessLayer;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace BusinessLogicLayer
{
    public static class RecipeBusinessLogic
    {
        public static DiabetesTrackerDbContext DbContext { get; set; }
        public static Recipe CreateRecipe(string name, string content)
        {
            Recipe newRecipe = new Recipe() 
            {
                UserId = UserBusinessLogic.GetCurrentUser(),
                Name = name,
                Content = content,
                CreatedOn = DateTime.Now,
            };
            DbContext.Recipes.Add(newRecipe);

            DbContext.SaveChanges();

            return newRecipe;
        }
    }
}