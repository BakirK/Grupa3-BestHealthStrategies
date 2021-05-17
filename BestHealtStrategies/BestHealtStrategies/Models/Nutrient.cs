using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BestHealtStrategies.Models
{
    public class Nutrient
    {
        public Nutrient(int calories, int carbohydrates, int fat, int protein)
        {
            Calories = calories;
            Carbohydrates = carbohydrates;
            Fat = fat;
            Protein = protein;
        }
        [Required]
        public int Calories { get; set; }
        [Required]
        public int Carbohydrates { get; set; }
        [Required]
        public int Fat { get; set; }
        [Required]
        public int Protein { get; set; }
    }
}
