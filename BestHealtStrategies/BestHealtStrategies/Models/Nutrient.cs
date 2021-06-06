using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BestHealtStrategies.Models
{
    public class Nutrient
    {
        public Nutrient() { }

        [Key]
        public int Id { get; set; }
        [Required]
        public double Calories { get; set; }
        [Required]
        public double Carbohydrates { get; set; }
        [Required]
        public double Fat { get; set; }
        [Required]
        public double Protein { get; set; }
        [ForeignKey("DailyMealPlan")]
        public int? DailyMealPlanId { get; set; }
        public DailyMealPlan DailyMealPlan { get; set; }

    }
}
