using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BestHealtStrategies.Models
{
    public class Nutrient
    {
        public Nutrient(int calories, int carbs, int fats, int proteins)
        {
            Calories = calories;
            Carbs = carbs;
            Fats = fats;
            Proteins = proteins;
        }
        [Required]
        public int Calories { get; set; }
        [Required]
        public int Carbs { get; set; }
        [Required]
        public int Fats { get; set; }
        [Required]
        public int Proteins { get; set; }
    }
}
