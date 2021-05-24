using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static BestHealtStrategies.Models.ValueObjects.ValueObjects;

namespace BestHealtStrategies.Models
{
    public class ProgressHistory
    {
        public ProgressHistory() { }
        public ProgressHistory(
            int id,
            DateTime date, 
            int weight, 
            ActivityLevel activity, 
            double bmi, 
            double height, 
            int age, 
            int userID)
        {
            ID = id;
            Date = date;
            Weight = weight;
            Activity = activity;
            Bmi = bmi;
            Height = height;
            Age = age;
            UserID = userID;
        }
        [Key]
        public int ID { get; set; }
        [Required, DataType(DataType.DateTime)]
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
        [Required, NotMapped]
        public User User { get; set; }
        [Required, ForeignKey("User")]
        public int UserID { get; set; }
    }
}
