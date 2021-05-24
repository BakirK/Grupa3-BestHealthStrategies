using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BestHealtStrategies.Models
{
    public class Rating
    {
        public Rating() { }

        public Rating(
            int iD, 
            int value,
            DateTime date, 
            int dailyMealId, 
            DailyMealPlan dailyMealPlan, 
            int userID, 
            User user)
        {
            ID = iD;
            Value = value;
            Date = date;
            DailyMealId = dailyMealId;
            DailyMealPlan = dailyMealPlan;
            UserID = userID;
            User = user;
        }

        [Key]
        public int ID { get; set; }
        [Required, Range(0, 5, ErrorMessage = "Rating value must be between 0 and 5")]
        public int Value
        {
            get { return this.Value; }

            set
            {
                if (value < 0 || value > 5)
                    throw new ArgumentOutOfRangeException("Value out of range");
                Value = value;
            }
        }
        [Required, DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Required, ForeignKey("DailyMealPlan")]
        public int DailyMealId { get; set; }
        public DailyMealPlan DailyMealPlan { get; set; }
        [Required, ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
