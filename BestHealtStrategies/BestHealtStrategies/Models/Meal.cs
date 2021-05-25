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
        public Meal() { }
        public Meal(
            int iD, 
            string title, 
            string image, 
            int readyInMinutes, 
            int servings, 
            string sourceURL, 
            DailyMealPlan dailyMealPlan,
            int? mealPlanID, 
            string summary, 
            string instructions)
        {
            ID = iD;
            Title = title;
            Image = image;
            ReadyInMinutes = readyInMinutes;
            Servings = servings;
            SourceURL = sourceURL;
            DailyMealPlan = dailyMealPlan;
            MealPlanID = mealPlanID;
            Summary = summary;
            Instructions = instructions;
        }

        [Key]
        public int ID { get; set; }
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
        [ForeignKey("DailyMealPlan")]
        public int? MealPlanID { get; set; }
        [Required]
        public DailyMealPlan DailyMealPlan { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public string Instructions { get; set; }
    }
}
