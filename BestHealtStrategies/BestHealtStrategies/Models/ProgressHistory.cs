using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static BestHealtStrategies.Models.ValueObjects.ValueObjects;

namespace BestHealtStrategies.Models
{
    public class ProgressHistory
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required, EnumDataType(typeof(ActivityLevel))]
        public ActivityLevel Activity { get; set; }
        [Required]
        public double Bmi { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public int Age { get; set; }
    }
}
