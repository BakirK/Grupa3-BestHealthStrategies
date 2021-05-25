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
        public DailyMealPlan(
            int iD, 
            int? userId, 
            User user, 
            DateTime startDate, 
            DateTime endDate, 
            Nutrient nutrient, 
            List<Meal> meals
            )
        {
            ID = iD;
            UserID = userId;
            User = user;
            StartDate = startDate;
            EndDate = endDate;
            Nutrient = nutrient;
            Meals = meals;
        }
        public DailyMealPlan() { }

        [Key, Required]
        public int ID { get; set; }
        [ForeignKey("User")]
        public int? UserID { get; set; }
        public User User { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
        [InverseProperty("DailyMealPlan")]
        public Nutrient Nutrient { get; set; }
        [InverseProperty("DailyMealPlan")]
        public List<Meal> Meals { get; set; }
        [InverseProperty("DailyMealPlan")]
        public List<Rating> Raitings { get; set; }
    }
}
