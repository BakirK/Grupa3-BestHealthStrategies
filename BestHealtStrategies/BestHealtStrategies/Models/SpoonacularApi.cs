using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Net.Http;
using System.Text.Json;
using Newtonsoft.Json;
using static BestHealtStrategies.Models.ValueObjects.ValueObjects;
using Newtonsoft.Json.Linq;

namespace BestHealtStrategies.Models
{
    public class SpoonacularApi : ApiCalls
    {
        public SpoonacularApi()
        {
        }

        public static string ApiKey { get; set; } = "51efcf710ac84199993836a5d7bcd080";
        private static readonly Lazy<SpoonacularApi> lazy = new Lazy<SpoonacularApi>(() => new SpoonacularApi());
        // private UriTemplate template = new UriTemplate("/mealplanner/generate?timeFrame=week&targetCalories={cal}&diet={diet}&exclude={intolerances}&apiKey=51efcf710ac84199993836a5d7bcd080");
        private static Uri Prefix = new Uri("https://api.spoonacular.com");

        public static SpoonacularApi Instance {
            get
            {
                return lazy.Value;
            }
        }
        public Meal getMeal(int id)
        {
            UriBuilder builder = new UriBuilder("https://api.spoonacular.com/recipes/" + id + "/information?apiKey=51efcf710ac84199993836a5d7bcd080");
            HttpClient client = new HttpClient();
            var result = client.GetAsync(builder.Uri).Result;
            using (StreamReader sr = new StreamReader(result.Content.ReadAsStreamAsync().Result))
            {
                Console.WriteLine(sr.ReadToEnd());
                Meal meal = JsonConvert.DeserializeObject<Meal>(sr.ReadToEnd());
                return meal;
            }
        }

        public List<DailyMealPlan> getWeeklyMealPlan(User user)
        {
            List<DailyMealPlan> weeklyMealPlan = new List<DailyMealPlan>();
            UriBuilder builder = new UriBuilder("https://api.spoonacular.com/mealplanner/generate");
            string query = "timeFrame=week&targetCalories=" + user.TargetCalories + "&diet=" + 
                            user.Diet.ToString() + "&apiKey=" + ApiKey + "&exclude=egg";
            builder.Query = query;
            /*foreach (Intolerance intolerance in user.Intolerances)
            {
                query += intolerance.ToString() + ",";
            }*/
            HttpClient client = new HttpClient();
            var result = client.GetAsync(builder.Uri).Result;
            using (StreamReader sr = new StreamReader(result.Content.ReadAsStreamAsync().Result))
            {
                JObject json = JObject.Parse(sr.ReadToEnd());
                // parse week array of daily meals
                IEnumerable<JToken> week = json.SelectToken("week");

                foreach (JToken dayy in week)
                {
                    // parse nutrient object
                    // select self because c#
                    JToken day = dayy.First();
                    JToken nutrients = day.SelectToken("nutrients");
                    Console.WriteLine(nutrients);
                    Console.WriteLine(nutrients.ToString());
                    Nutrient nutrient = JsonConvert.DeserializeObject<Nutrient>(nutrients.ToString());

                    DailyMealPlan dailyMealPlan = new DailyMealPlan();
                    //todo - id za daily meal plan
                    dailyMealPlan.StartDate = DateTime.Now;
                    dailyMealPlan.EndDate = DateTime.Now.AddDays(7);
                    dailyMealPlan.User = user;
                    dailyMealPlan.UserId = user.Id;
                    dailyMealPlan.Nutrient = nutrient;

                    List<Meal> dailyMeals = new List<Meal>();

                    // parse json array of 3 daily meals
                    IEnumerable<JToken> meals = day.SelectTokens("meals");
                    foreach (JToken m in meals)
                    {
                        // select self because c#
                        JToken mealToken = m.First();
                        int id = (int)mealToken.SelectToken("id");
                        Meal meal = getMeal(id);
                        dailyMeals.Add(meal);
                    }
                    dailyMealPlan.Meals = dailyMeals;

                    weeklyMealPlan.Add(dailyMealPlan);
                }
                return weeklyMealPlan;
            }
        }
    }
}
