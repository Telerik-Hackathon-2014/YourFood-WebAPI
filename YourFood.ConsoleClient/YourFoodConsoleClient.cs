namespace YourFood.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using ReceiptScannerOCRS;
    using YourFood.Data.DbContext;
    using YourFood.Data.UoW;
    using YourFood.EverliveAPI;
    using YourFood.Models;
    using YourFood.Models.Enums;

    public class YourFoodConsoleClient
    {
        private static readonly IYourFoodData yourFoodData = new YourFoodData(new YourFoodDbContext());
        private static readonly ReceiptScannerTesseract receiptScanner = new ReceiptScannerTesseract();

        internal static void Main()
        {
            var textLines = receiptScanner.GetLines();
            Console.WriteLine(string.Join(Environment.NewLine, textLines));

            //SeedProductCategories();
            //SeedProducts();
            //SeedCatalogProducts();
            //SeedRecipeCategories();
            //SeedRecipes();
            //SeedTags();
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
                //    return;
            }

            string recipeImagesFolderPath = "..\\..\\images\\recipe-images\\";

            // Strawberry pretzel salad
            var strawberryPretzelSalad = new Recipe()
            {
                Name = "Strawberry Pretzel Salad",
                CategoryId = yourFoodData.RecipeCategories.All().FirstOrDefault(rc => rc.Name == "Desert").Id,
                Description = "Preheat oven to 350 degrees F (175 degrees C).\nCream butter or margarine with the brown sugar. Mix in the pretzels and pat mixture into the bottom of one 9x13 inch baking pan. Bake at 350 degrees F (175 degrees C) for 10 to 12 minutes. Set aside to cool.\nIn a medium bowl, dissolve the gelatin in the boiling water and stir in the strawberries. Chill until partially thickened.\nIn a small bowl beat the cream cheese and white sugar together until smooth. Fold in the whipped cream. Spread mixture over the top of the cooled crust, making sure to seal the edges. Chill then pour the gelatin mixture over he cream cheese layer. Chill until firm.",
                ImageUrl = GetUploadedImageUrl(recipeImagesFolderPath + "strawberry-pretzel-salad.jpg"),
                TimeToMakeInMinutes = 105
            };

            strawberryPretzelSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Butter").Id,
                Quantity = 0.75,
                UnitType = UnitType.Cups
            });

            strawberryPretzelSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Brown sugar").Id,
                Quantity = 3,
                UnitType = UnitType.Tablespoons
            });

            strawberryPretzelSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Pretzel").Id,
                Quantity = 2.5,
                UnitType = UnitType.Cups
            });

            strawberryPretzelSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Jell-O").Id,
                Quantity = 170,
                UnitType = UnitType.Grams
            });

            strawberryPretzelSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Water").Id,
                Quantity = 2,
                UnitType = UnitType.Cups
            });

            strawberryPretzelSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Strawberry").Id,
                Quantity = 3,
                UnitType = UnitType.Cups
            });

            strawberryPretzelSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Cream cheese").Id,
                Quantity = 225,
                UnitType = UnitType.Grams
            });

            strawberryPretzelSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Sugar").Id,
                Quantity = 1,
                UnitType = UnitType.Cups
            });

            strawberryPretzelSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Whip cream").Id,
                Quantity = 225,
                UnitType = UnitType.Grams
            });

            yourFoodData.Recipes.Add(strawberryPretzelSalad);
            
            // Soft chocolate cip cookies
            var softChocolateChipCookies = new Recipe()
            {
                Name = "Soft Chocolate Chip Cookies",
                CategoryId = yourFoodData.RecipeCategories.All().FirstOrDefault(rc => rc.Name == "Desert").Id,
                Description = "Preheat oven to 350 degrees F (175 degrees C). Sift together the flour and baking soda, set aside.\nIn a large bowl, cream together the butter, brown sugar, and white sugar. Beat in the instant pudding mix until blended. Stir in the eggs and vanilla. Blend in the flour mixture. Finally, stir in the chocolate chips and nuts. Drop cookies by rounded spoonfuls onto ungreased cookie sheets.\nBake for 10 to 12 minutes in the preheated oven. Edges should be golden brown.",
                ImageUrl = GetUploadedImageUrl(recipeImagesFolderPath + "soft-chocolate-chip-cookies.jpg"),
                TimeToMakeInMinutes = 100
            };

            softChocolateChipCookies.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Flour").Id,
                Quantity = 4.5,
                UnitType = UnitType.Cups
            });

            softChocolateChipCookies.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Baking soda").Id,
                Quantity = 2,
                UnitType = UnitType.Teaspoons
            });

            softChocolateChipCookies.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Butter").Id,
                Quantity = 2,
                UnitType = UnitType.Cups
            });

            softChocolateChipCookies.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Brown sugar").Id,
                Quantity = 1.5,
                UnitType = UnitType.Cups
            });

            softChocolateChipCookies.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Sugar").Id,
                Quantity = 0.5,
                UnitType = UnitType.Cups
            });

            softChocolateChipCookies.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Pudding mix").Id,
                Quantity = 100,
                UnitType = UnitType.Grams
            });

            softChocolateChipCookies.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Egg").Id,
                Quantity = 4,
                UnitType = UnitType.Pieces
            });

            softChocolateChipCookies.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Vanilla").Id,
                Quantity = 2,
                UnitType = UnitType.Teaspoons
            });

            softChocolateChipCookies.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Chocolate chips").Id,
                Quantity = 4,
                UnitType = UnitType.Cups
            });

            softChocolateChipCookies.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Walnut").Id,
                Quantity = 2,
                UnitType = UnitType.Cups
            });

            yourFoodData.Recipes.Add(softChocolateChipCookies);

            // Soft chocolate cip cookies
            var zucchiniPatties = new Recipe()
            {
                Name = "Zucchini Patties",
                CategoryId = yourFoodData.RecipeCategories.All().FirstOrDefault(rc => rc.Name == "Appetizer").Id,
                Description = "In a medium bowl, combine the zucchini, eggs, onion, flour, Parmesan cheese, mozzarella cheese, and salt. Stir well enough to distribute ingredients evenly.\nHeat a small amount of oil in a skillet over medium-high heat. Drop zucchini mixture by heaping tablespoonfuls, and cook for a few minutes on each side until golden.",
                ImageUrl = GetUploadedImageUrl(recipeImagesFolderPath + "zucchini-patties.jpg"),
                TimeToMakeInMinutes = 30
            };

            zucchiniPatties.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Zucchini").Id,
                Quantity = 2,
                UnitType = UnitType.Cups
            });

            zucchiniPatties.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Egg").Id,
                Quantity = 2,
                UnitType = UnitType.Pieces
            });

            zucchiniPatties.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Onion").Id,
                Quantity = 0.25,
                UnitType = UnitType.Cups
            });

            zucchiniPatties.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Flour").Id,
                Quantity = 0.5,
                UnitType = UnitType.Cups
            });

            zucchiniPatties.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Parmesan").Id,
                Quantity = 0.5,
                UnitType = UnitType.Cups
            });

            zucchiniPatties.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Mozzarella").Id,
                Quantity = 0.5,
                UnitType = UnitType.Cups
            });

            zucchiniPatties.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Salt").Id,
                Quantity = 0,
                UnitType = UnitType.Cups
            });

            zucchiniPatties.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Olive oil").Id,
                Quantity = 2,
                UnitType = UnitType.Tablespoons
            });
            
            yourFoodData.Recipes.Add(zucchiniPatties);

            // Baked lemon chicken with mushroom sauce
            var bakedLemonChickenWithMushroomSauce = new Recipe()
            {
                Name = "Baked Lemon Chicken with Mushroom Sauce",
                CategoryId = yourFoodData.RecipeCategories.All().FirstOrDefault(rc => rc.Name == "Main dish").Id,
                Description = "Preheat oven to 400 degrees F (205 degrees C).\nPour olive oil in an 8x8-inch glass baking dish. Place the chicken breasts in the dish, coating each side with oil. Squeeze the juice of 1/2 lemon over each chicken breast. Slice the rest of the lemon and place a lemon slice on top of each chicken piece.\nBake in the preheated oven until no longer pink in the center and the juices run clear, 30 to 40 minutes. An instant-read thermometer inserted into the center should read at least 165 degrees F (74 degrees C).\nMelt butter in a skillet over medium heat; add mushrooms. Cook and stir until mushrooms are brown and liquid is evaporated, about 6 minutes. Sprinkle flour over mushrooms and stir until coated. Add chicken broth, stirring to make a medium-thick sauce. Allow sauce to reduce, adjusting with a little more broth to make a creamy sauce. Add fresh parsley at the last minute. Spoon the sauce over the baked chicken breasts.",
                ImageUrl = GetUploadedImageUrl(recipeImagesFolderPath + "baked-lemon-chicken-with-mushroom-sauce.jpg"),
                TimeToMakeInMinutes = 55
            };

            bakedLemonChickenWithMushroomSauce.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Olive oil").Id,
                Quantity = 1,
                UnitType = UnitType.Tablespoons
            });

            bakedLemonChickenWithMushroomSauce.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Chicken breast").Id,
                Quantity = 6,
                UnitType = UnitType.Pieces
            });

            bakedLemonChickenWithMushroomSauce.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Lemon").Id,
                Quantity = 1,
                UnitType = UnitType.Pieces
            });

            bakedLemonChickenWithMushroomSauce.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Butter").Id,
                Quantity = 0.25,
                UnitType = UnitType.Cups
            });

            bakedLemonChickenWithMushroomSauce.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Mushroom").Id,
                Quantity = 3,
                UnitType = UnitType.Cups
            });

            bakedLemonChickenWithMushroomSauce.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Flour").Id,
                Quantity = 2,
                UnitType = UnitType.Tablespoons
            });

            bakedLemonChickenWithMushroomSauce.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Chicken broth").Id,
                Quantity = 0.5,
                UnitType = UnitType.Cups
            });

            bakedLemonChickenWithMushroomSauce.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Parsley").Id,
                Quantity = 1,
                UnitType = UnitType.Tablespoons
            });

            yourFoodData.Recipes.Add(bakedLemonChickenWithMushroomSauce);

            // Buttermilk pancakes
            var buttermilkPancakes = new Recipe()
            {
                Name = "Buttermilk Pancakes",
                CategoryId = yourFoodData.RecipeCategories.All().FirstOrDefault(rc => rc.Name == "Desert").Id,
                Description = "In a large bowl, combine flour, sugar, baking powder, baking soda, and salt. In a separate bowl, beat together buttermilk, milk, eggs and melted butter. Keep the two mixtures separate until you are ready to cook.\nHeat a lightly oiled griddle or frying pan over medium high heat. You can flick water across the surface and if it beads up and sizzles, it's ready!\nPour the wet mixture into the dry mixture, using a wooden spoon or fork to blend. Stir until it's just blended together. Do not over stir! Pour or scoop the batter onto the griddle, using approximately 1/2 cup for each pancake. Brown on both sides and serve hot.",
                ImageUrl = GetUploadedImageUrl(recipeImagesFolderPath + "buttermilk-pancakes.jpg"),
                TimeToMakeInMinutes = 25
            };

            buttermilkPancakes.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Flour").Id,
                Quantity = 3,
                UnitType = UnitType.Cups
            });

            buttermilkPancakes.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Sugar").Id,
                Quantity = 3,
                UnitType = UnitType.Tablespoons
            });

            buttermilkPancakes.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Baking powder").Id,
                Quantity = 3,
                UnitType = UnitType.Teaspoons
            });

            buttermilkPancakes.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Baking soda").Id,
                Quantity = 1.5,
                UnitType = UnitType.Teaspoons
            });

            buttermilkPancakes.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Salt").Id,
                Quantity = 0.75,
                UnitType = UnitType.Teaspoons
            });

            buttermilkPancakes.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Buttermilk").Id,
                Quantity = 3,
                UnitType = UnitType.Cups
            });

            buttermilkPancakes.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Milk").Id,
                Quantity = 0.5,
                UnitType = UnitType.Cups
            });

            buttermilkPancakes.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Egg").Id,
                Quantity = 3,
                UnitType = UnitType.Pieces
            });

            buttermilkPancakes.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Butter").Id,
                Quantity = 0.33,
                UnitType = UnitType.Cups
            });

            yourFoodData.Recipes.Add(buttermilkPancakes);

            // Baked Pork Chops
            var bakedPorkChops = new Recipe()
            {
                Name = "Baked Pork Chops",
                CategoryId = yourFoodData.RecipeCategories.All().FirstOrDefault(rc => rc.Name == "Main dish").Id,
                Description = "Preheat oven to 350 degrees F (175 degrees C).\nRinse pork chops, pat dry, and season with garlic powder and seasoning salt to taste. Place the beaten eggs in a small bowl. Dredge the pork chops lightly in flour, dip in the egg, and coat liberally with bread crumbs.\nHeat the oil in a medium skillet over medium-high heat. Fry the pork chops 5 minutes per side, or until the breading appears well browned. Transfer the chops to a 9x13 inch baking dish, and cover with foil.\nBake in the preheated oven for 1 hour. While baking, combine the cream of mushroom soup, milk and white wine in a medium bowl. After the pork chops have baked for an hour, cover them with the soup mixture. Replace foil, and bake for another 30 minutes.",
                ImageUrl = GetUploadedImageUrl(recipeImagesFolderPath + "baked-pork-chops.jpg"),
                TimeToMakeInMinutes = 120
            };

            bakedPorkChops.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Pork").Id,
                Quantity = 6,
                UnitType = UnitType.Pieces
            });

            bakedPorkChops.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Garlic powder").Id,
                Quantity = 1,
                UnitType = UnitType.Teaspoons
            });

            bakedPorkChops.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Salt").Id,
                Quantity = 1,
                UnitType = UnitType.Teaspoons
            });

            bakedPorkChops.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Egg").Id,
                Quantity = 2,
                UnitType = UnitType.Pieces
            });

            bakedPorkChops.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Flour").Id,
                Quantity = 0.25,
                UnitType = UnitType.Cups
            });

            bakedPorkChops.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Bread crumb").Id,
                Quantity = 2,
                UnitType = UnitType.Cups
            });

            bakedPorkChops.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Olive oil").Id,
                Quantity = 4,
                UnitType = UnitType.Tablespoons
            });

            bakedPorkChops.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Mushroom sauce").Id,
                Quantity = 150,
                UnitType = UnitType.Grams
            });

            bakedPorkChops.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Milk").Id,
                Quantity = 0.5,
                UnitType = UnitType.Cups
            });

            bakedPorkChops.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "White wine").Id,
                Quantity = 0.33,
                UnitType = UnitType.Cups
            });

            yourFoodData.Recipes.Add(bakedPorkChops);

            // Quinoa and Black Beans
            var quinoaAndBlackBeans = new Recipe()
            {
                Name = "Quinoa and Black Beans",
                CategoryId = yourFoodData.RecipeCategories.All().FirstOrDefault(rc => rc.Name == "Vegetarian").Id,
                Description = "Heat oil in a saucepan over medium heat; cook and stir onion and garlic until lightly browned, about 10 minutes.\nMix quinoa into onion mixture and cover with vegetable broth; season with cumin, cayenne pepper, salt, and pepper. Bring the mixture to a boil. Cover, reduce heat, and simmer until quinoa is tender and broth is absorbed, about 20 minutes.\nStir frozen corn into the saucepan, and continue to simmer until heated through, about 5 minutes; mix in the black beans and coriander.",
                ImageUrl = GetUploadedImageUrl(recipeImagesFolderPath + "quinoa-and-black-beans.jpg"),
                TimeToMakeInMinutes = 50
            };

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Olive oil").Id,
                Quantity = 1,
                UnitType = UnitType.Teaspoons
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Onion").Id,
                Quantity = 1,
                UnitType = UnitType.Pieces
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Garlic").Id,
                Quantity = 3,
                UnitType = UnitType.Pieces
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Quinoa").Id,
                Quantity = 0.75,
                UnitType = UnitType.Cups
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Vegetable broth").Id,
                Quantity = 1.5,
                UnitType = UnitType.Cups
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Cumin").Id,
                Quantity = 1,
                UnitType = UnitType.Teaspoons
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Red pepper").Id,
                Quantity = 0.25,
                UnitType = UnitType.Teaspoons
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Black pepper").Id,
                Quantity = 0,
                UnitType = UnitType.Grams
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Salt").Id,
                Quantity = 0,
                UnitType = UnitType.Grams
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Corn").Id,
                Quantity = 1,
                UnitType = UnitType.Cups
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Canned beans").Id,
                Quantity = 425,
                UnitType = UnitType.Grams
            });

            quinoaAndBlackBeans.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Coriander").Id,
                Quantity = 0.5,
                UnitType = UnitType.Cups
            });

            yourFoodData.Recipes.Add(quinoaAndBlackBeans);

            // Deviled Eggs
            var deviledEggs = new Recipe()
            {
                Name = "Deviled Eggs",
                CategoryId = yourFoodData.RecipeCategories.All().FirstOrDefault(rc => rc.Name == "Appetizer").Id,
                Description = "Place eggs in a pot of salted water. Bring the water to a boil, and let eggs cook in boiling water until they are hard boiled, approximately 10 to 15 minutes. Drain eggs, and let cool. Once cool, remove the shell, cut the eggs in half lengthwise and scoop out the yolks.\nPlace the yolks in a medium-size mixing bowl and mash them. Blend in vinegar, mayonnaise, mustard, salt and pepper. You may need to add more mayonnaise to hold the mixture together, but it should be slightly dry.\nCarefully put the egg yolk mixture back into the egg whites but do not pack it. There will be enough mixture so the whites are overfilled. Sprinkle with paprika. Place on bed of lettuce and/or garnish with parsley. Cool before serving.",
                ImageUrl = GetUploadedImageUrl(recipeImagesFolderPath + "deviled-eggs.jpg"),
                TimeToMakeInMinutes = 35
            };

            deviledEggs.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Egg").Id,
                Quantity = 6,
                UnitType = UnitType.Pieces
            });

            deviledEggs.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Vinegar").Id,
                Quantity = 1,
                UnitType = UnitType.Teaspoons
            });

            deviledEggs.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Mayonnaise").Id,
                Quantity = 1,
                UnitType = UnitType.Tablespoons
            });

            deviledEggs.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Mustard").Id,
                Quantity = 0.25,
                UnitType = UnitType.Teaspoons
            });

            deviledEggs.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Salt").Id,
                Quantity = 0,
                UnitType = UnitType.Grams
            });

            deviledEggs.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Black pepper").Id,
                Quantity = 0,
                UnitType = UnitType.Grams
            });

            deviledEggs.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Paprika").Id,
                Quantity = 1,
                UnitType = UnitType.Teaspoons
            });

            deviledEggs.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Lettuce").Id,
                Quantity = 2,
                UnitType = UnitType.Pieces
            });

            yourFoodData.Recipes.Add(deviledEggs);

            // Tangy Cucumber and Avocado Salad
            var tangyCucumberAndAvocadoSalad = new Recipe()
            {
                Name = "Tangy Cucumber and Avocado Salad",
                CategoryId = yourFoodData.RecipeCategories.All().FirstOrDefault(rc => rc.Name == "Salad").Id,
                Description = "In a large bowl, combine cucumbers, avocados, and cilantro. Stir in garlic, onions, salt, and pepper. Squeeze lemon and lime over the top, and toss. Cover, and refrigerate at least 30 minutes.",
                ImageUrl = GetUploadedImageUrl(recipeImagesFolderPath + "tangy-cucumber-and-avocado-salad.jpg"),
                TimeToMakeInMinutes = 45
            };

            tangyCucumberAndAvocadoSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Cucumber").Id,
                Quantity = 2,
                UnitType = UnitType.Pieces
            });

            tangyCucumberAndAvocadoSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Avocado").Id,
                Quantity = 2,
                UnitType = UnitType.Pieces
            });

            tangyCucumberAndAvocadoSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Coriander").Id,
                Quantity = 4,
                UnitType = UnitType.Tablespoons
            });

            tangyCucumberAndAvocadoSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Garlic").Id,
                Quantity = 1,
                UnitType = UnitType.Pieces
            });

            tangyCucumberAndAvocadoSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Onion").Id,
                Quantity = 2,
                UnitType = UnitType.Tablespoons
            });

            tangyCucumberAndAvocadoSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Salt").Id,
                Quantity = 0.25,
                UnitType = UnitType.Teaspoons
            });

            tangyCucumberAndAvocadoSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Black pepper").Id,
                Quantity = 0,
                UnitType = UnitType.Grams
            });

            tangyCucumberAndAvocadoSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Lemon").Id,
                Quantity = 0.25,
                UnitType = UnitType.Pieces
            });

            tangyCucumberAndAvocadoSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Lime").Id,
                Quantity = 1,
                UnitType = UnitType.Pieces
            });

            yourFoodData.Recipes.Add(tangyCucumberAndAvocadoSalad);

            // Refreshing Cucumber Watermelon Salad
            var refreshingCucumberWatermelonSalad = new Recipe()
            {
                Name = "Refreshing Cucumber Watermelon Salad",
                CategoryId = yourFoodData.RecipeCategories.All().FirstOrDefault(rc => rc.Name == "Salad").Id,
                Description = "Mix red onion with lime juice in a bowl; set side to marinate at least 10 minutes. Stir olive oil into mixture.\nToss watermelon, baby cucumbers, and feta cheese together in a large bowl. Pour the red onion mixture over the watermelon mixture; toss to coat. Sprinkle mint over the salad; toss.",
                ImageUrl = GetUploadedImageUrl(recipeImagesFolderPath + "refreshing-cucumber-watermelon-salad.jpg"),
                TimeToMakeInMinutes = 25
            };

            refreshingCucumberWatermelonSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Red onion").Id,
                Quantity = 1,
                UnitType = UnitType.Pieces
            });

            refreshingCucumberWatermelonSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Lime juice").Id,
                Quantity = 2,
                UnitType = UnitType.Tablespoons
            });

            refreshingCucumberWatermelonSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Olive oil").Id,
                Quantity = 2,
                UnitType = UnitType.Tablespoons
            });

            refreshingCucumberWatermelonSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Watermelon").Id,
                Quantity = 1,
                UnitType = UnitType.Pieces
            });

            refreshingCucumberWatermelonSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Cucumber").Id,
                Quantity = 3,
                UnitType = UnitType.Pieces
            });

            refreshingCucumberWatermelonSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Feta cheese").Id,
                Quantity = 1,
                UnitType = UnitType.Cups
            });

            refreshingCucumberWatermelonSalad.Ingredients.Add(new RecipeProduct()
            {
                ProductId = yourFoodData.Products.All().FirstOrDefault(p => p.Name == "Mint").Id,
                Quantity = 0.5,
                UnitType = UnitType.Cups
            });

            yourFoodData.Recipes.Add(refreshingCucumberWatermelonSalad);
            
            yourFoodData.SaveChanges();
            Console.WriteLine("Recipies added.");
        }

        private static void SeedRecipeCategories()
        {
            if (yourFoodData.RecipeCategories.All().Any())
            {
                //   return;
            }

            var recipeCategoryNames = new string[] { "Main dish", "Appetizer", "Desert", "Salad", "Soup", "Vegetarian", "Beverage" };

            for (int i = 0; i < recipeCategoryNames.Length; i++)
            {
                var recipeCategory = new RecipeCategory()
                {
                    Name = recipeCategoryNames[i]
                };

                yourFoodData.RecipeCategories.Add(recipeCategory);
            }

            yourFoodData.SaveChanges();
            Console.WriteLine("Recipe categories added.");
        }

        private static void SeedCatalogProducts()
        {
            if (yourFoodData.CatalogProducts.All().Any())
            {
                // return;
            }

            Dictionary<string, int> productLifetimes = new Dictionary<string, int>();
            productLifetimes.Add("Apple", 45);
            productLifetimes.Add("Avocado", 8);
            productLifetimes.Add("Baking powder", 999);
            productLifetimes.Add("Baking soda", 999);
            productLifetimes.Add("Banana", 5);
            productLifetimes.Add("Beans", 999);
            productLifetimes.Add("Beef", 3);
            productLifetimes.Add("Black pepper", 999);
            productLifetimes.Add("Blueberry", 6);
            productLifetimes.Add("Bread crumb", 180);
            productLifetimes.Add("Broccoli", 4);
            productLifetimes.Add("Brown sugar", 999);
            productLifetimes.Add("Butter", 30);
            productLifetimes.Add("Buttermilk", 14);
            productLifetimes.Add("Cabbage", 8);
            productLifetimes.Add("Canned beans", 4);
            productLifetimes.Add("Carrot", 14);
            productLifetimes.Add("Chicken breast", 2);
            productLifetimes.Add("Chicken", 2);
            productLifetimes.Add("Chicken broth", 999);
            productLifetimes.Add("Chocolate", 100);
            productLifetimes.Add("Chocolate chips", 100);
            productLifetimes.Add("Coriander", 9);
            productLifetimes.Add("Corn", 7);
            productLifetimes.Add("Cream cheese", 23);
            productLifetimes.Add("Cucumber", 7);
            productLifetimes.Add("Cumin", 999);
            productLifetimes.Add("Egg", 30);
            productLifetimes.Add("Flour", 200);
            productLifetimes.Add("Feta cheese", 7);
            productLifetimes.Add("Fig", 6);
            productLifetimes.Add("Garlic", 4 * 30);
            productLifetimes.Add("Garlic powder", 999);
            productLifetimes.Add("Grapefruit", 5);
            productLifetimes.Add("Grapes", 7);
            productLifetimes.Add("Iceberg salad", 7);
            productLifetimes.Add("Jell-O", 3 * 30);
            productLifetimes.Add("Ketchup", 365);
            productLifetimes.Add("Lemon", 2 * 7);
            productLifetimes.Add("Lettuce", 7);
            productLifetimes.Add("Lime", 2 * 7);
            productLifetimes.Add("Lime juice", 7);
            productLifetimes.Add("Mayonnaise", 7);
            productLifetimes.Add("Milk", 7);
            productLifetimes.Add("Mint", 10);
            productLifetimes.Add("Mozzarella", 10);
            productLifetimes.Add("Mushroom", 7);
            productLifetimes.Add("Mushroom sauce", 7);
            productLifetimes.Add("Mustard", 365);
            productLifetimes.Add("Olive Oil", 365);
            productLifetimes.Add("Onion", 45);
            productLifetimes.Add("Orange", 45);
            productLifetimes.Add("Paprika", 999);
            productLifetimes.Add("Parmesan", 90);
            productLifetimes.Add("Parsley", 6);
            productLifetimes.Add("Peach", 7);
            productLifetimes.Add("Pear", 7);
            productLifetimes.Add("Pesto", 12);
            productLifetimes.Add("Pork", 2);
            productLifetimes.Add("Potato", 35);
            productLifetimes.Add("Pretzel", 25);
            productLifetimes.Add("Pudding mix", 999);
            productLifetimes.Add("Quinoa", 999);
            productLifetimes.Add("Red onion", 45);
            productLifetimes.Add("Red pepper", 999);
            productLifetimes.Add("Salamy", 7);
            productLifetimes.Add("Salmon", 2);
            productLifetimes.Add("Salt", 999);
            productLifetimes.Add("Sugar", 999);
            //     productLifetimes.Add("Pepper", 18);
            productLifetimes.Add("Sausage", 7);
            productLifetimes.Add("Spinach", 6);
            productLifetimes.Add("Strawberry", 5);
            productLifetimes.Add("Tomato", 12);
            productLifetimes.Add("Turkey", 2);
            productLifetimes.Add("Vanilla", 999);
            productLifetimes.Add("Vegetable broth", 999);
            productLifetimes.Add("Vinegar", 999);
            productLifetimes.Add("Walnut", 180);
            productLifetimes.Add("Water", 999);
            productLifetimes.Add("Watermelon", 14);
            productLifetimes.Add("Whip cream", 14);
            productLifetimes.Add("White cheese", 7);
            productLifetimes.Add("White wine", 365);
            productLifetimes.Add("Yellow cheese", 30);
            productLifetimes.Add("Zucchini", 6);

            foreach (var item in productLifetimes)
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
                //     return;
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
            var otherCategory = yourFoodData.ProductCategories.All().FirstOrDefault(c => c.Name == "Other");

            Tuple<string, string, ProductCategory, UnitType>[] productsInfo = new Tuple<string, string, ProductCategory, UnitType>[]
            {
                new Tuple<string, string, ProductCategory, UnitType>("Apple", "apple.jpg", fruitCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Avocado", "avocado.jpg", fruitCategory, UnitType.Pieces),
                new Tuple<string, string, ProductCategory, UnitType>("Baking powder", "baking-powder.jpg", otherCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Baking soda", "baking-soda.jpg", otherCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Banana", "banana.jpg", fruitCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Beans", "beans.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Beef", "beef.jpg", meatCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Black pepper", "salt-pepper-shaker.jpg", seasoningCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Blueberry", "blueberry.jpg", fruitCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Bread crumb", "bread-crumb.jpg", pastryCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Broccoli", "broccoli.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Brown sugar", "brown-sugar.jpg", seasoningCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Butter", "butter.jpg", dairyCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Buttermilk", "buttermilk.jpg", dairyCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Cabbage", "cabbage.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Canned beans", "canned-beans.jpg", cannedCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Carrot", "carrot.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Chicken breast", "chicken-breast.jpg", meatCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Chicken", "chicken.jpg", meatCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Chicken broth", "chicken-broth.jpg", flavorCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Chocolate", "chocolate.jpg", flavorCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Chocolate chips", "chocolate-chips.jpg", flavorCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Coriander", "coriander.jpg", seasoningCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Corn", "corn.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Cream cheese", "cream-cheese.jpg", dairyCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Cucumber", "cucumber.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Cumin", "cumin.jpg", seasoningCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Egg", "egg.jpg", dairyCategory, UnitType.Pieces),
                new Tuple<string, string, ProductCategory, UnitType>("Feta cheese", "feta-cheese.jpg", dairyCategory, UnitType.Pieces),
                new Tuple<string, string, ProductCategory, UnitType>("Flour", "flour.jpg", pastryCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Fig", "fig.jpg", fruitCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Garlic", "garlic.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Garlic powder", "garlic-powder.jpg", flavorCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Grapefruit", "grapefruit.jpg", fruitCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Grapes", "grapes.jpg", fruitCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Iceberg salad", "iceberg-salad.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Jell-O", "jell-o.jpg", fruitCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Ketchup", "ketchup.jpg", sauceCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Lemon", "lemon.jpg", fruitCategory, UnitType.Pieces),
                new Tuple<string, string, ProductCategory, UnitType>("Lettuce", "lettuce.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Lime", "lime.jpg", fruitCategory, UnitType.Pieces),
                new Tuple<string, string, ProductCategory, UnitType>("Lime juice", "lime-juice.jpg", fruitCategory, UnitType.Pieces),
                new Tuple<string, string, ProductCategory, UnitType>("Mayonnaise", "mayonnaise.jpg", sauceCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Milk", "milk.jpg", dairyCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Mint", "mint.jpg", fruitCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Mozzarella", "mozzarella.jpg", dairyCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Mustard", "mustard.jpg", sauceCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Mushroom", "mushroom.jpg", vegetableCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Mushroom sauce", "mushroom-sauce.jpg", sauceCategory, UnitType.Grams),
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
                new Tuple<string, string, ProductCategory, UnitType>("Strawberry", "strawberry.jpg", fruitCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Sugar", "cane-sugar.jpg", flavorCategory, UnitType.Grams),
                new Tuple<string, string, ProductCategory, UnitType>("Tomato", "tomato.jpg", vegetableCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Turkey", "turkey.jpg", meatCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Vanilla", "vanilla.jpg", flavorCategory, UnitType.Kilograms),
                new Tuple<string, string, ProductCategory, UnitType>("Vegetable broth", "vegetable-broth.jpg", flavorCategory, UnitType.Kilograms),
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
            string productsImagesFolderPath = "../../images/product-images/";

            //Parallel.For(0, productsInfo.Length, (i) =>
            //{
            //    var fileBytes = File.ReadAllBytes(productsImagesFolderPath + productsInfo[i].Item2);

            //    using (MemoryStream ms = new MemoryStream(fileBytes))
            //    {
            //        string pictureUrl = uploader.UrlFromMemoryStream(ms);
            //        yourFoodData.Products.Add(new Product()
            //        {
            //            Name = productsInfo[i].Item1,
            //            ImageUrl = pictureUrl,
            //            CategoryId = productsInfo[i].Item3.Id,
            //            UnitType = productsInfo[i].Item4
            //        });
            //    }
            //});

            for (int i = 0; i < productsInfo.Length; i++)
            {
                var fileBytes = File.ReadAllBytes(productsImagesFolderPath + productsInfo[i].Item2);

                using (MemoryStream ms = new MemoryStream(fileBytes))
                {
                    string pictureUrl = uploader.UrlFromMemoryStream(ms);
                    yourFoodData.Products.Add(new Product()
                    {
                        Name = productsInfo[i].Item1,
                        ImageUrl = pictureUrl,
                        CategoryId = productsInfo[i].Item3.Id,
                        UnitType = productsInfo[i].Item4
                    });
                }
            }

            yourFoodData.SaveChanges();
            Console.WriteLine("Products added.");          
        }

        private static string GetUploadedImageUrl(string imagePath)
        {
            ImageUploader uploader = new ImageUploader();
            var fileBytes = File.ReadAllBytes(imagePath);
            string imageUrl = "";

            using (MemoryStream ms = new MemoryStream(fileBytes))
            {
                imageUrl = uploader.UrlFromMemoryStream(ms);
            }

            return imageUrl;
        }

        private static void SeedProductCategories()
        {
            if (yourFoodData.ProductCategories.All().Any())
            {
                //     return;
            }

            string[] categoryNames = 
            {
                "Meat", "Fish", "Dairy", "Fruit", "Vegetable", "Sauce", "Cooked", "Canned", "Drink", "Nut", "Flavor", "Pastry", "Spice", "Dressing", "Other"
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

        private static void SeedTags()
        {
            if (yourFoodData.Tags.All().Any())
            {
                return;
            }

            var productNames = yourFoodData.Products
                                           .All()
                                           .Select(p => new
                                           {
                                               Id = p.Id,
                                               Word = p.Name.ToLower()
                                           });

            foreach (var product in productNames)
            {
                var matches = Regex.Matches(product.Word, @"\b\w{2,}\b");
                var splittedWords = matches.Cast<Match>().Select(m => m.Value);

                foreach (var word in splittedWords)
                {
                    yourFoodData.Tags.Add(new Tag()
                    {
                        ProductId = product.Id,
                        Word = word
                    });
                }
            }

            yourFoodData.Tags.SaveChanges();
        }
    }
}