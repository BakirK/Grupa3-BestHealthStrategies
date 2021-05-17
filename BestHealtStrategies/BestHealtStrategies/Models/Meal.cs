using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BestHealtStrategies.Models
{
    public class Meal
    {
        public Meal(
            int iD,
            string title,
            string imageURL,
            int readyInMinutes,
            int servings,
            string sourceURL,
            DailyMealPlan mealPlan,
            string summary,
            string instructions
            )
        {
            ID = iD;
            Title = title;
            Image = imageURL;
            ReadyInMinutes = readyInMinutes;
            Servings = servings;
            SourceURL = sourceURL;
            MealPlan = mealPlan;
            Summary = summary;
            Instructions = instructions;
        }
        [Key, Required]
        public int ID { get; }
        [Required, RegularExpression(@"[a-zA-Z ]+")]
        public string Title { get; set; }
        [Required, Url]
        public string Image { get; set; }
        [Required]
        public int ReadyInMinutes { get; set; }
        [Required]
        public int Servings { get; set; }
        [Required, Url]
        public string SourceURL { get; set; }
        [Required, ForeignKey("DailyMealPlan")]
        public int MealPlanID { get; set; }
        [Required]
        public DailyMealPlan MealPlan { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public string Instructions { get; set; }
    }
}
