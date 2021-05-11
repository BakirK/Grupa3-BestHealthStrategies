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
        public Rating(int value, DateTime date, int dailyMealId, int userID)
        {
            if (value < 0 || value > 5)
                    throw new ArgumentOutOfRangeException("Value out of range");
            Value = value;
            Date = date;
            DailyMealId = dailyMealId;
            UserID = userID;
        }
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
        // TODO fk
        [Required, ForeignKey("DailyMealId")]
        public int DailyMealId { get; set; }
        [Required, ForeignKey("DailyMealId")]
        public int UserID { get; set; }
    }
}
