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
using System.Text;

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

        public static SpoonacularApi Instance
        {
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
                Meal meal = JsonConvert.DeserializeObject<Meal>(sr.ReadToEnd());
                return meal;
            }
        }

        public List<DailyMealPlan> getWeeklyMealPlan(User user)
        {
            List<DailyMealPlan> weeklyMealPlan = new List<DailyMealPlan>();
            UriBuilder builder = new UriBuilder("https://api.spoonacular.com/mealplanner/generate");
            StringBuilder query = new StringBuilder("timeFrame=week&targetCalories=" + user.TargetCalories + "&diet=" +
                            user.Diet.ToString() + "&apiKey=" + ApiKey + "&exclude=egg");
            if (user.Intolerances != null)
            {
                foreach (Intolerance intolerance in user.Intolerances)
                {
                    query.Append(intolerance.ToString() + ",");
                    query.Remove(query.Length - 1, 1);
                }
            }
            builder.Query = query.ToString();

            HttpClient client = new HttpClient();
            var result = client.GetAsync(builder.Uri).Result;
            using (StreamReader sr = new StreamReader(result.Content.ReadAsStreamAsync().Result))
            {
                JObject json = JObject.Parse(sr.ReadToEnd());
                // parse week array of daily meals
                IEnumerable<JToken> week = json.SelectToken("week");
                if (week == null)
                {
                    //api limit reached
                    throw new Exception("Daily API limit reached");
                }
                foreach (JToken dayy in week)
                {
                    // parse nutrient object
                    // select self because c#
                    JToken day = dayy.First();
                    JToken nutrients = day.SelectToken("nutrients");
                    Nutrient nutrient = JsonConvert.DeserializeObject<Nutrient>(nutrients.ToString());

                    DailyMealPlan dailyMealPlan = new DailyMealPlan();
                    nutrient.DailyMealPlan = dailyMealPlan;
                    //todo - id za daily meal plan
                    dailyMealPlan.StartDate = DateTime.Now;
                    dailyMealPlan.EndDate = DateTime.Now.AddDays(7);
                    dailyMealPlan.User = user;
                    dailyMealPlan.UserId = user.Id;
                    dailyMealPlan.Nutrient = nutrient;

                    List<Meal> dailyMeals = new List<Meal>();

                    // parse json array of 3 daily meals
                    IEnumerable<JToken> meals = day.SelectTokens("meals").First();
                    foreach (JToken m in meals)
                    {
                        int id = (int)m.SelectToken("id");
                        Meal meal = getMeal(id);
                        meal.DailyMealPlan = dailyMealPlan;
                        //meal.MealPlanID = dailyMealPlan.Id;
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
