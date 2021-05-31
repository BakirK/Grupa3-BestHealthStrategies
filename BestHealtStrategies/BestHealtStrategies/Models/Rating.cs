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
            int? dailyMealId, 
            DailyMealPlan dailyMealPlan,
            string userId, 
            User user)
        {
            Id = iD;
            Value = value;
            Date = date;
            DailyMealId = dailyMealId;
            DailyMealPlan = dailyMealPlan;
            UserId = userId;
            User = user;
        }

        [Key]
        public int Id { get; set; }
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
        [ForeignKey("DailyMealPlan")]
        public int? DailyMealId { get; set; }
        public DailyMealPlan DailyMealPlan { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
