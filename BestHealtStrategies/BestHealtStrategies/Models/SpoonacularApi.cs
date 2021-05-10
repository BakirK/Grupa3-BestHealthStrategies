using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.ServiceModel;

namespace BestHealtStrategies.Models
{
    public class SpoonacularApi : ApiCalls
    {
        public SpoonacularApi()
        {
        }

        public static string ApiKey { get; set; } = "51efcf710ac84199993836a5d7bcd080";
        private static readonly Lazy<SpoonacularApi> lazy = new Lazy<SpoonacularApi>(() => new SpoonacularApi());
        private UriTemplate template = new UriTemplate("/mealplanner/generate?timeFrame=week&targetCalories={cal}&diet={diet}&exclude={intolerances}&apiKey=51efcf710ac84199993836a5d7bcd080");
        private static Uri Prefix = new Uri("https://api.spoonacular.com");

        public static SpoonacularApi Instance {
            get
            {
                return lazy.Value;
            }
        }
        public Meal getMeal(int id)
        {
            throw new NotImplementedException();
        }

        public List<DailyMealPlan> getWeeklyMealPlan(User user)
        {
            // TODO

            throw new NotImplementedException();
        }

        private async Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            using Stream stream = response.GetResponseStream();
            using StreamReader reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }
    }
}
