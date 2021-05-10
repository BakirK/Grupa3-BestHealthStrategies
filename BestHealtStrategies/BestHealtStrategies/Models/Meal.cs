using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BestHealtStrategies.Models
{
    public class Meal
    {
        public Meal(int iD, string title, string imageURL, int readyInMinutes, int servings, string sourceURL)
        {
            ID = iD;
            Title = title;
            ImageURL = imageURL;
            ReadyInMinutes = readyInMinutes;
            Servings = servings;
            SourceURL = sourceURL;
        }
        [Key, Required]
        public int ID { get; }
        [Required, RegularExpression(@"[a-zA-Z ]+")]
        public string Title { get; set; }
        [Required, Url]
        public string ImageURL { get; set; }
        [Required]
        public int ReadyInMinutes { get; set; }
        [Required]
        public int Servings { get; set; }
        [Required, Url]
        public string SourceURL { get; set; }

    }
}
