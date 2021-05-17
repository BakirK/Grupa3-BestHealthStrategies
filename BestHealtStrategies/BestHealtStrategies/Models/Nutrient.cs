using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BestHealtStrategies.Models
{
    public class Nutrient
    {
        public Nutrient(double calories, double carbohydrates, double fat, double protein)
        {
            Calories = calories;
            Carbohydrates = carbohydrates;
            Fat = fat;
            Protein = protein;
        }
        [Required]
        public double Calories { get; set; }
        [Required]
        public double Carbohydrates { get; set; }
        [Required]
        public double Fat { get; set; }
        [Required]
        public double Protein { get; set; }
    }
}
