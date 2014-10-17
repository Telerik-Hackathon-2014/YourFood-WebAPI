namespace YourFood.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using YourFood.Data.DbContext;
    using YourFood.Data.UoW;
    using YourFood.EverliveAPI;
    using YourFood.Models;
    using YourFood.Models.Enums;

    public class YourFoodConsoleClient
    {
        private static readonly IYourFoodData yourFoodData = new YourFoodData(new YourFoodDbContext());

        internal static void Main()
        {
            //SeedProductCategories();
            //SeedProducts();
            //SeedCatalogProducts();
            //SeedRecipeCategories();
            //SeedRecipes();
        }
 
        private static void SeedProductInDatabase()
        {
            if (yourFoodData.CatalogProducts.All().Any())
            {
                return;
            }

            yourFoodData.CatalogProducts.Add(new CatalogProduct()
            {
                LifetimeInDays = 5,
                Product = new Product()
                {
                    Category = new ProductCategory()
                    {
                        Name = "Food"
                    },
                    ImageUrl = "sample",
                    UnitType = UnitType.Kilograms,
                    MeasurementUnit = 5,
                    Name = "eggs"
                }
            });

            yourFoodData.SaveChanges();

            Console.WriteLine(yourFoodData.CatalogProducts.All().First().LifetimeInDays);
        }

        private static void SeedRecipes()
        {
            if (yourFoodData.Recipes.All().Any())
            {
                return;
            }

            // Strawberry pretzel salad
            var strawberryPretzelSalad = new Recipe()
            {
                Name = "Strawberry Pretzel Salad",
                CategoryId = yourFoodData.RecipeCategoriess.All().FirstOrDefault(rc => rc.Name == "Desert").Id,
                Description = "Preheat oven to 350 degrees F (175 degrees C).\nCream butter or margarine with the brown sugar. Mix in the pretzels and pat mixture into the bottom of one 9x13 inch baking pan. Bake at 350 degrees F (175 degrees C) for 10 to 12 minutes. Set aside to cool.\nIn a medium bowl, dissolve the gelatin in the boiling water and stir in the strawberries. Chill until partially thickened.\nIn a small bowl beat the cream cheese and white sugar together until smooth. Fold in the whipped cream. Spread mixture over the top of the cooled crust, making sure to seal the edges. Chill then pour the gelatin mixture over he cream cheese layer. Chill until firm.",
                ImageUrl = "",
                TimeToMakeInMinutes = 105
            };

            strawberryPretzelSalad.Ingredients.Add(new RecipeProduct()
                {
                    ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Butter").Id,
                    Quantity = 225,
                });

            strawberryPretzelSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Brown sugar").Id,
                Quantity = 15,
            });

            strawberryPretzelSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Pretzel").Id,
                Quantity = 700,
            });

            strawberryPretzelSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Jell-O").Id,
                Quantity = 170,
            });

            strawberryPretzelSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Water").Id,
                Quantity = 600,
            });

            strawberryPretzelSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Strawberry").Id,
                Quantity = 450,
            });

            strawberryPretzelSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Cream cheese").Id,
                Quantity = 225,
            });

            strawberryPretzelSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Sugar").Id,
                Quantity = 200,
            });

            strawberryPretzelSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Whip cream").Id,
                Quantity = 225,
            });

            yourFoodData.Recipes.Add(strawberryPretzelSalad);
            
            // Soft chocolate cip cookies
            var softChocolateChipCookies = new Recipe()
            {
                Name = "Soft Chocolate Chip Cookies",
                CategoryId = yourFoodData.RecipeCategoriess.All().FirstOrDefault(rc => rc.Name == "Desert").Id,
                Description = "Preheat oven to 350 degrees F (175 degrees C). Sift together the flour and baking soda, set aside.\nIn a large bowl, cream together the butter, brown sugar, and white sugar. Beat in the instant pudding mix until blended. Stir in the eggs and vanilla. Blend in the flour mixture. Finally, stir in the chocolate chips and nuts. Drop cookies by rounded spoonfuls onto ungreased cookie sheets.\nBake for 10 to 12 minutes in the preheated oven. Edges should be golden brown.",
                ImageUrl = "",
                TimeToMakeInMinutes = 100
            };

            softChocolateChipCookies.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Flour").Id,
                Quantity = 575,
            });

            softChocolateChipCookies.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Baking soda").Id,
                Quantity = 10,
            });

            softChocolateChipCookies.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Butter").Id,
                Quantity = 450,
            });

            softChocolateChipCookies.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Brown sugar").Id,
                Quantity = 300,
            });

            softChocolateChipCookies.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Sugar").Id,
                Quantity = 100,
            });

            softChocolateChipCookies.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Pudding mix").Id,
                Quantity = 100,
            });

            softChocolateChipCookies.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Egg").Id,
                Quantity = 4,
            });

            softChocolateChipCookies.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Vanilla").Id,
                Quantity = 9,
            });

            softChocolateChipCookies.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Chocolate cips").Id,
                Quantity = 500,
            });

            softChocolateChipCookies.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Walnut").Id,
                Quantity = 215,
            });

            yourFoodData.Recipes.Add(softChocolateChipCookies);

            // Soft chocolate cip cookies
            var zucchiniPatties = new Recipe()
            {
                Name = "Zucchini Patties",
                CategoryId = yourFoodData.RecipeCategoriess.All().FirstOrDefault(rc => rc.Name == "Appetizer").Id,
                Description = "In a medium bowl, combine the zucchini, eggs, onion, flour, Parmesan cheese, mozzarella cheese, and salt. Stir well enough to distribute ingredients evenly.\nHeat a small amount of oil in a skillet over medium-high heat. Drop zucchini mixture by heaping tablespoonfuls, and cook for a few minutes on each side until golden.",
                ImageUrl = "",
                TimeToMakeInMinutes = 30
            };

            zucchiniPatties.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Zucchini").Id,
                Quantity = 300,
            });

            zucchiniPatties.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Egg").Id,
                Quantity = 2,
            });

            zucchiniPatties.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Onion").Id,
                Quantity = 37,
            });

            zucchiniPatties.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Flour").Id,
                Quantity = 68,
            });

            zucchiniPatties.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Parmesan").Id,
                Quantity = 60,
            });

            zucchiniPatties.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Mozzarella").Id,
                Quantity = 56,
            });

            zucchiniPatties.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Salt").Id,
                Quantity = 0,
            });

            zucchiniPatties.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Olive oil").Id,
                Quantity = 9,
            });
            
            yourFoodData.Recipes.Add(zucchiniPatties);

            // Baked lemon chicken with mushroom sauce
            var bakedLemonChickenWithMushroomSauce = new Recipe()
            {
                Name = "Baked Lemon Chicken with Mushroom Sauce",
                CategoryId = yourFoodData.RecipeCategoriess.All().FirstOrDefault(rc => rc.Name == "Main dish").Id,
                Description = "Preheat oven to 400 degrees F (205 degrees C).\nPour olive oil in an 8x8-inch glass baking dish. Place the chicken breasts in the dish, coating each side with oil. Squeeze the juice of 1/2 lemon over each chicken breast. Slice the rest of the lemon and place a lemon slice on top of each chicken piece.\nBake in the preheated oven until no longer pink in the center and the juices run clear, 30 to 40 minutes. An instant-read thermometer inserted into the center should read at least 165 degrees F (74 degrees C).\nMelt butter in a skillet over medium heat; add mushrooms. Cook and stir until mushrooms are brown and liquid is evaporated, about 6 minutes. Sprinkle flour over mushrooms and stir until coated. Add chicken broth, stirring to make a medium-thick sauce. Allow sauce to reduce, adjusting with a little more broth to make a creamy sauce. Add fresh parsley at the last minute. Spoon the sauce over the baked chicken breasts.",
                ImageUrl = "",
                TimeToMakeInMinutes = 55
            };

            bakedLemonChickenWithMushroomSauce.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Olive oil").Id,
                Quantity = 15,
            });

            bakedLemonChickenWithMushroomSauce.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Chicken breast").Id,
                Quantity = 1000,
            });

            bakedLemonChickenWithMushroomSauce.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Lemon").Id,
                Quantity = 1,
            });

            bakedLemonChickenWithMushroomSauce.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Butter").Id,
                Quantity = 56,
            });

            bakedLemonChickenWithMushroomSauce.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Mushroom").Id,
                Quantity = 300,
            });

            bakedLemonChickenWithMushroomSauce.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Flour").Id,
                Quantity = 20,
            });

            bakedLemonChickenWithMushroomSauce.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Chicken broth").Id,
                Quantity = 240,
            });

            bakedLemonChickenWithMushroomSauce.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Parsley").Id,
                Quantity = 11,
            });

            yourFoodData.Recipes.Add(bakedLemonChickenWithMushroomSauce);

            // Buttermilk pancakes
            var buttermilkPancakes = new Recipe()
            {
                Name = "Buttermilk Pancakes",
                CategoryId = yourFoodData.RecipeCategoriess.All().FirstOrDefault(rc => rc.Name == "Desert").Id,
                Description = "In a large bowl, combine flour, sugar, baking powder, baking soda, and salt. In a separate bowl, beat together buttermilk, milk, eggs and melted butter. Keep the two mixtures separate until you are ready to cook.\nHeat a lightly oiled griddle or frying pan over medium high heat. You can flick water across the surface and if it beads up and sizzles, it's ready!\nPour the wet mixture into the dry mixture, using a wooden spoon or fork to blend. Stir until it's just blended together. Do not over stir! Pour or scoop the batter onto the griddle, using approximately 1/2 cup for each pancake. Brown on both sides and serve hot.",
                ImageUrl = "",
                TimeToMakeInMinutes = 25
            };

            buttermilkPancakes.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Flour").Id,
                Quantity = 385,
            });

            buttermilkPancakes.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Sugar").Id,
                Quantity = 45,
            });

            buttermilkPancakes.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Baking powder").Id,
                Quantity = 45,
            });

            buttermilkPancakes.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Baking soda").Id,
                Quantity = 10,
            });

            buttermilkPancakes.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Salt").Id,
                Quantity = 4,
            });

            buttermilkPancakes.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Buttermilk").Id,
                Quantity = 720,
            });

            buttermilkPancakes.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Milk").Id,
                Quantity = 125,
            });

            buttermilkPancakes.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Egg").Id,
                Quantity = 3,
            });

            buttermilkPancakes.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Butter").Id,
                Quantity = 75,
            });

            yourFoodData.Recipes.Add(buttermilkPancakes);

            // Baked Pork Chops
            var bakedPorkChops = new Recipe()
            {
                Name = "Baked Pork Chops",
                CategoryId = yourFoodData.RecipeCategoriess.All().FirstOrDefault(rc => rc.Name == "Main dish").Id,
                Description = "Preheat oven to 350 degrees F (175 degrees C).\nRinse pork chops, pat dry, and season with garlic powder and seasoning salt to taste. Place the beaten eggs in a small bowl. Dredge the pork chops lightly in flour, dip in the egg, and coat liberally with bread crumbs.\nHeat the oil in a medium skillet over medium-high heat. Fry the pork chops 5 minutes per side, or until the breading appears well browned. Transfer the chops to a 9x13 inch baking dish, and cover with foil.\nBake in the preheated oven for 1 hour. While baking, combine the cream of mushroom soup, milk and white wine in a medium bowl. After the pork chops have baked for an hour, cover them with the soup mixture. Replace foil, and bake for another 30 minutes.",
                ImageUrl = "",
                TimeToMakeInMinutes = 120
            };

            bakedPorkChops.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Pork").Id,
                Quantity = 1.6,
            });

            bakedPorkChops.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Garlic powder").Id,
                Quantity = 3,
            });

            bakedPorkChops.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Salt").Id,
                Quantity = 5,
            });

            bakedPorkChops.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Egg").Id,
                Quantity = 2,
            });

            bakedPorkChops.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Flour").Id,
                Quantity = 30,
            });

            bakedPorkChops.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Bread crumb").Id,
                Quantity = 250,
            });

            bakedPorkChops.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Olive oil").Id,
                Quantity = 56,
            });

            bakedPorkChops.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Mushroom sauce").Id,
                Quantity = 150,
            });

            bakedPorkChops.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Milk").Id,
                Quantity = 120,
            });

            bakedPorkChops.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "White wine").Id,
                Quantity = 80,
            });

            yourFoodData.Recipes.Add(bakedPorkChops);

            // Quinoa and Black Beans
            var quinoaAndBlackBeans = new Recipe()
            {
                Name = "Quinoa and Black Beans",
                CategoryId = yourFoodData.RecipeCategoriess.All().FirstOrDefault(rc => rc.Name == "Vegetarian").Id,
                Description = "Heat oil in a saucepan over medium heat; cook and stir onion and garlic until lightly browned, about 10 minutes.\nMix quinoa into onion mixture and cover with vegetable broth; season with cumin, cayenne pepper, salt, and pepper. Bring the mixture to a boil. Cover, reduce heat, and simmer until quinoa is tender and broth is absorbed, about 20 minutes.\nStir frozen corn into the saucepan, and continue to simmer until heated through, about 5 minutes; mix in the black beans and cilantro.",
                ImageUrl = "",
                TimeToMakeInMinutes = 50
            };

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Olive oil").Id,
                Quantity = 4,
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Onion").Id,
                Quantity = 60,
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Garlic").Id,
                Quantity = 15,
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Quinoa").Id,
                Quantity = 100,
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Vegetable broth").Id,
                Quantity = 340,
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Cumin").Id,
                Quantity = 3,
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Red pepper").Id,
                Quantity = 1,
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Black pepper").Id,
                Quantity = 0,
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Salt").Id,
                Quantity = 0,
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Corn").Id,
                Quantity = 175,
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Canned beans").Id,
                Quantity = 425,
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Cilantro").Id,
                Quantity = 25,
            });

            yourFoodData.Recipes.Add(quinoaAndBlackBeans);

            // Deviled Eggs
            var deviledEggs = new Recipe()
            {
                Name = "Deviled Eggs",
                CategoryId = yourFoodData.RecipeCategoriess.All().FirstOrDefault(rc => rc.Name == "Appetizer").Id,
                Description = "Place eggs in a pot of salted water. Bring the water to a boil, and let eggs cook in boiling water until they are hard boiled, approximately 10 to 15 minutes. Drain eggs, and let cool. Once cool, remove the shell, cut the eggs in half lengthwise and scoop out the yolks.\nPlace the yolks in a medium-size mixing bowl and mash them. Blend in vinegar, mayonnaise, mustard, salt and pepper. You may need to add more mayonnaise to hold the mixture together, but it should be slightly dry.\nCarefully put the egg yolk mixture back into the egg whites but do not pack it. There will be enough mixture so the whites are overfilled. Sprinkle with paprika. Place on bed of lettuce and/or garnish with parsley. Cool before serving.",
                ImageUrl = "",
                TimeToMakeInMinutes = 35
            };

            deviledEggs.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Egg").Id,
                Quantity = 6,
            });

            deviledEggs.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Vinegar").Id,
                Quantity = 15,
            });

            deviledEggs.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Mayonnaise").Id,
                Quantity = 15,
            });

            deviledEggs.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Mustard").Id,
                Quantity = 1.5,
            });

            deviledEggs.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Salt").Id,
                Quantity = 0,
            });

            deviledEggs.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Black pepper").Id,
                Quantity = 0,
            });

            deviledEggs.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Paprika").Id,
                Quantity = 2.2,
            });

            deviledEggs.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Lettuce").Id,
                Quantity = 0,
            });

            yourFoodData.Recipes.Add(deviledEggs);

            // Tangy Cucumber and Avocado Salad
            var tangyCucumberAndAvocadoSalad = new Recipe()
            {
                Name = "Tangy Cucumber and Avocado Salad",
                CategoryId = yourFoodData.RecipeCategoriess.All().FirstOrDefault(rc => rc.Name == "Salad").Id,
                Description = "In a large bowl, combine cucumbers, avocados, and cilantro. Stir in garlic, onions, salt, and pepper. Squeeze lemon and lime over the top, and toss. Cover, and refrigerate at least 30 minutes.",
                ImageUrl = "",
                TimeToMakeInMinutes = 45
            };

            tangyCucumberAndAvocadoSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Cucumber").Id,
                Quantity = 500,
            });

            tangyCucumberAndAvocadoSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Avocado").Id,
                Quantity = 2,
            });

            tangyCucumberAndAvocadoSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Cilantro").Id,
                Quantity = 6,
            });

            tangyCucumberAndAvocadoSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Garlic").Id,
                Quantity = 5,
            });

            tangyCucumberAndAvocadoSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Onion").Id,
                Quantity = 8,
            });

            tangyCucumberAndAvocadoSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Salt").Id,
                Quantity = 1.5,
            });

            tangyCucumberAndAvocadoSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Black pepper").Id,
                Quantity = 0,
            });

            tangyCucumberAndAvocadoSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Lemon").Id,
                Quantity = 0.25,
            });

            tangyCucumberAndAvocadoSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Lime").Id,
                Quantity = 1,
            });

            yourFoodData.Recipes.Add(tangyCucumberAndAvocadoSalad);

            // Refreshing Cucumber Watermelon Salad
            var refreshingCucumberWatermelonSalad = new Recipe()
            {
                Name = "Refreshing Cucumber Watermelon Salad",
                CategoryId = yourFoodData.RecipeCategoriess.All().FirstOrDefault(rc => rc.Name == "Salad").Id,
                Description = "Mix red onion with lime juice in a bowl; set side to marinate at least 10 minutes. Stir olive oil into mixture.\nToss watermelon, baby cucumbers, and feta cheese together in a large bowl. Pour the red onion mixture over the watermelon mixture; toss to coat. Sprinkle mint over the salad; toss.",
                ImageUrl = "",
                TimeToMakeInMinutes = 25
            };

            refreshingCucumberWatermelonSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Red onion").Id,
                Quantity = 100,
            });

            refreshingCucumberWatermelonSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Lime juice").Id,
                Quantity = 15,
            });

            refreshingCucumberWatermelonSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Olive oil").Id,
                Quantity = 15,
            });

            refreshingCucumberWatermelonSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Watermelon").Id,
                Quantity = 1000,
            });

            refreshingCucumberWatermelonSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Cucumber").Id,
                Quantity = 300,
            });

            refreshingCucumberWatermelonSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Feta cheese").Id,
                Quantity = 150,
            });

            refreshingCucumberWatermelonSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Mint").Id,
                Quantity = 10,
            });

            yourFoodData.Recipes.Add(refreshingCucumberWatermelonSalad);
            
            yourFoodData.SaveChanges();
            Console.WriteLine("Recipies added.");
        }

        private static void SeedRecipeCategories()
        {
            if (yourFoodData.RecipeCategoriess.All().Any())
            {
                return;
            }

            var recipeCategoryNames = new string[] { "Main dish", "Desert", "Salad", "Vegetarian" };

            for (int i = 0; i < recipeCategoryNames.Length; i++)
            {
                var recipeCategory = new RecipeCategory()
                {
                    Name = recipeCategoryNames[i]
                };

                yourFoodData.RecipeCategoriess.Add(recipeCategory);
            }

            yourFoodData.SaveChanges();
            Console.WriteLine("Recipe categories added.");
        }


        private static void SeedCatalogProducts()
        {
            Dictionary<string, int> productLifetimes = new Dictionary<string, int>();
            productLifetimes.Add("Apple", 1);
            productLifetimes.Add("Avocado", 1);
            productLifetimes.Add("Baking soda", 1);
            productLifetimes.Add("Banana", 1);
            productLifetimes.Add("Beans", 1);
            productLifetimes.Add("Beef", 1);
            productLifetimes.Add("Blueberry", 1);
            productLifetimes.Add("Broccoli", 1);
            productLifetimes.Add("Brown sugar", 1);
            productLifetimes.Add("Butter", 1);
            productLifetimes.Add("Cabbage", 1);
            productLifetimes.Add("Carrot", 1);
            productLifetimes.Add("Chicken breast", 1);
            productLifetimes.Add("Chicken", 1);
            productLifetimes.Add("Chocolate", 1);
            productLifetimes.Add("Cilantaro", 1);
            productLifetimes.Add("Corn", 1);
            productLifetimes.Add("Cucumber", 1);
            productLifetimes.Add("Cumin", 1);
            productLifetimes.Add("Eggs", 1);
            productLifetimes.Add("Flour", 1);
            productLifetimes.Add("Fig", 1);
            productLifetimes.Add("Garlic", 1);
            productLifetimes.Add("Grapefruit", 1);
            productLifetimes.Add("Grapes", 1);
            productLifetimes.Add("Iceberg salad", 1);
            productLifetimes.Add("Jell-O", 1);
            productLifetimes.Add("Ketchup", 1);
            productLifetimes.Add("Lemon", 1);
            productLifetimes.Add("Lettuce", 1);
            productLifetimes.Add("Lime", 1);
            productLifetimes.Add("Mayonnaise", 1);
            productLifetimes.Add("Milk", 1);
            productLifetimes.Add("Mint", 1);
            productLifetimes.Add("Mozzarella", 1);
            productLifetimes.Add("Mustard", 1);
            productLifetimes.Add("Olive Oil", 1);
            productLifetimes.Add("Onion", 1);
            productLifetimes.Add("Orange", 1);
            productLifetimes.Add("Paprika", 1);
            productLifetimes.Add("Parmesan", 1);
            productLifetimes.Add("Parsley", 1);
            productLifetimes.Add("Peach", 1);
            productLifetimes.Add("Pear", 1);
            productLifetimes.Add("Pesto", 1);
            productLifetimes.Add("Pork", 1);
            productLifetimes.Add("Potato", 1);
            productLifetimes.Add("Pretzel", 1);
            productLifetimes.Add("Quinoa", 1);
            productLifetimes.Add("Red onion", 1);
            productLifetimes.Add("Red pepper", 1);
            productLifetimes.Add("Salamy", 1);
            productLifetimes.Add("Salmon", 1);
            productLifetimes.Add("Salt", 1);
            productLifetimes.Add("Pepper", 1);
            productLifetimes.Add("Sausage", 1);
            productLifetimes.Add("Spinach", 1);
            productLifetimes.Add("Strawberries", 1);
            productLifetimes.Add("Tomato", 1);
            productLifetimes.Add("Turkey", 1);
            productLifetimes.Add("Vanilla", 1);
            productLifetimes.Add("Vinegar", 1);
            productLifetimes.Add("Walnut", 1);
            productLifetimes.Add("Water melon", 1);
            productLifetimes.Add("Whip cream", 1);
            productLifetimes.Add("White cheese", 1);
            productLifetimes.Add("White wine", 1);
            productLifetimes.Add("Yellow cheese", 1);
            productLifetimes.Add("Zucchini", 1);

            foreach(var item in productLifetimes)
            {
                var product = yourFoodData.Products.All().FirstOrDefault(p => p.Name == item.Key);

                yourFoodData.CatalogProducts.Add(new CatalogProduct()
                    {
                        ProductId = product.Id,
                        LifetimeInDays = item.Value
                    });
            }

            yourFoodData.SaveChanges();

            Console.WriteLine("Catalog products added.");
        }

        private static void SeedProducts()
        {
            if (yourFoodData.Products.All().Any())
            {
                return;
            }

            var meatCategory = yourFoodData.ProductCategories.All().FirstOrDefault(c => c.Name == "Meat");
            var dairyCategory = yourFoodData.ProductCategories.All().FirstOrDefault(c => c.Name == "Dairy");
            var fishCategory = yourFoodData.ProductCategories.All().FirstOrDefault(c => c.Name == "Fish");
            var fruitCategory = yourFoodData.ProductCategories.All().FirstOrDefault(c => c.Name == "Fruit");
            var vegetableCategory = yourFoodData.ProductCategories.All().FirstOrDefault(c => c.Name == "Vegetable");
            var sauceCategory = yourFoodData.ProductCategories.All().FirstOrDefault(c => c.Name == "Sauce");
            var cookedCategory = yourFoodData.ProductCategories.All().FirstOrDefault(c => c.Name == "Cooked");
            var cannedCategory = yourFoodData.ProductCategories.All().FirstOrDefault(c => c.Name == "Canned");
            var seasoningCategory = yourFoodData.ProductCategories.All().FirstOrDefault(c => c.Name == "Spice");
            var drinkCategory = yourFoodData.ProductCategories.All().FirstOrDefault(c => c.Name == "Drink");
            var nutCategory = yourFoodData.ProductCategories.All().FirstOrDefault(c => c.Name == "Nut");
            var flavorCategory = yourFoodData.ProductCategories.All().FirstOrDefault(c => c.Name == "Flavor");
            var pastryCategory = yourFoodData.ProductCategories.All().FirstOrDefault(c => c.Name == "Pastry");

            Tuple<string, string, ProductCategory, UnitType>[] productsInfo = new Tuple<string, string, ProductCategory, UnitType>[]
            {
                new Tuple<string, string, ProductCategory, UnitType>("Apple", "apple.jpg", fruitCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Avocado", "avocado.jpg", fruitCategory, UnitType.Pieces),
                new Tuple<string, string, ProductCategory, UnitType>("Baking soda", "baking-soda.jpg", seasoningCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Banana", "banana.jpg", fruitCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Beans", "beans.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Beef", "beef.jpg", meatCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Blueberry", "blueberry.jpg", fruitCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Broccoli", "broccoli.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Brown sugar", "brown-sugar.jpg", seasoningCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Butter", "butter.jpg", dairyCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Cabbage", "cabbage.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Carrot", "carrot.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Chicken breast", "chicken-breast.jpg", meatCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Chicken", "chicken.jpg", meatCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Chocolate", "chocolate.jpg", flavorCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Chocolate chips", "chocolate-chips.jpg", flavorCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Cilantaro", "cilantaro.jpg", seasoningCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Corn", "corn.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Cream cheese", "cream-cheese.jpg", dairyCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Cucumber", "cucumber.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Cumin", "cumin.jpg", seasoningCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Eggs", "eggs.jpg", dairyCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Flour", "flour.jpg", pastryCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Fig", "fig.jpg", fruitCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Garlic", "garlic.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Grapefruit", "grapefruit.jpg", fruitCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Grapes", "grapes.jpg", fruitCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Iceberg salad", "iceberg-salad.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Jell-O", "jell-o.jpg", fruitCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Ketchup", "ketchup.jpg", sauceCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Lemon", "lemon.jpg", fruitCategory, UnitType.Pieces),
                new Tuple<string, string, ProductCategory, UnitType>("Lettuce", "lettuce.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Lime", "lime.jpg", fruitCategory, UnitType.Pieces),
                new Tuple<string, string, ProductCategory, UnitType>("Mayonnaise", "mayonnaise.jpg", sauceCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Milk", "milk.jpg", dairyCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Mint", "mint.jpg", fruitCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Mozzarella", "mozzarella.jpg", dairyCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Mustard", "mustard.jpg", sauceCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Mushroom", "mushroom.jpg", vegetableCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Olive Oil", "olive-oil.jpg", flavorCategory, UnitType.Milliliters),
                new Tuple<string, string, ProductCategory, UnitType>("Onion", "onion.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Orange", "orange.jpg", fruitCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Paprika", "paprika.jpg", seasoningCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Parmesan", "parmesan.jpg", dairyCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Parsley", "parsley.jpg", vegetableCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Peach", "peach.jpg", fruitCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Pear", "pear.jpg", fruitCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Pesto", "pesto.jpg", sauceCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Pork", "pork.jpg", meatCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Potato", "potato.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Pretzel", "pretzel.jpg", pastryCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Pudding mix", "pudding-mix.jpg", flavorCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Quinoa", "quinoa.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Red onion", "red-onion.jpg", fruitCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Red pepper", "red-pepper.jpg", seasoningCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Salamy", "salamy.jpg", meatCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Salmon", "salmon.jpg", fishCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Salt", "salt-pepper-shaker.jpg", seasoningCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Black pepper", "salt-pepper-shaker.jpg", seasoningCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Sausage", "sausage.jpg", meatCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Spinach", "spinach.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Strawberries", "strawberries.jpg", fruitCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Sugar", "sugar.jpg", flavorCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Tomato", "tomato.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Turkey", "turkey.jpg", meatCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Vanilla", "vanilla.jpg", flavorCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Vinegar", "vinegar.jpg", flavorCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Walnut", "walnut.jpg", nutCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Watermelon", "water-melon.jpg", fruitCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Water", "water.jpg", drinkCategory, UnitType.Milliliters),
                new Tuple<string, string, ProductCategory, UnitType>("Whip cream", "whip-cream.jpg", dairyCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("White cheese", "white-cheese.jpg", dairyCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("White wine", "white-wine.jpg", drinkCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Yellow cheese", "yellow-cheese.jpg", dairyCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Zucchini", "zucchini.jpg", vegetableCategory, UnitType.Kilograms)
            };
                        
            ImageUploader uploader = new ImageUploader();

            for (int i = 0; i < productsInfo.Length; i++)
            {
                var fileBytes = File.ReadAllBytes(productsInfo[i].Item2);
                using (MemoryStream ms = new MemoryStream(fileBytes))
                {
                    string pictureUrl = uploader.UrlFromMemoryStream(ms);
                    yourFoodData.Products.Add(new Product()
                        {
                            Name = productsInfo[i].Item1,
                            ImageUrl = uploader.UrlFromMemoryStream(ms),
                            CategoryId = productsInfo[i].Item3.Id,
                            UnitType = productsInfo[i].Item4
                        });
                }
            }

            yourFoodData.SaveChanges();
            Console.WriteLine("Products added.");          
        }

        private static void SeedProductCategories()
        {
            if (yourFoodData.ProductCategories.All().Any())
            {
                return;
            }

            string[] categoryNames = 
            {
                "Meat", "Fish", "Dairy", "Fruit", "Vegetable", "Sauce", "Cooked", "Canned", "Drink", "Nut", "Flavor", "Pastry" 
            };

            string[] categoryImages = { "" };

            for (int i = 0; i < categoryNames.Length; i++)
            {
                yourFoodData.ProductCategories.Add(new ProductCategory()
                    {
                        Name = categoryNames[i]
                    });
            }

            yourFoodData.SaveChanges();
            Console.WriteLine("Product categories added.");
        }
    }   
}