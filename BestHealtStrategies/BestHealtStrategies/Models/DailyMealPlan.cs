using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BestHealtStrategies.Models
{
    public class DailyMealPlan
    {
        public DailyMealPlan(int iD, DateTime startDate, DateTime endDate, Nutrient nutrient, List<Meal> meals)
        {
            ID = iD;
            StartDate = startDate;
            EndDate = endDate;
            Nutrient = nutrient;
            Meals = meals;
        }
        [Key, Required]
        public int ID { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public Nutrient Nutrient { get; set; }
        [Required, NotMapped]
        public List<Meal> Meals { get; set; }
    }
}
