using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BestHealtStrategies.Models.ValueObjects
{
    public class ValueObjects
    {
        public enum Intolerance
        {
            [Display(Name = "Dairy products")]
            DAIRY,

            [Display(Name = "Eggs")]
            EGG,

            [Display(Name = "Gluten")]
            GLUTEN,

            [Display(Name = "Grain")]
            GRAIN,

            [Display(Name = "Peanut")]
            PEANUT,
            [Display(Name = "Seafood")]
            SEAFOOD,
            [Display(Name = "Sesame")]
            SESAME,
            [Display(Name = "Shellfish")]
            SHELLFISH,
            [Display(Name = "Soy")]
            SOY,
            [Display(Name = "Sulfite")]
            SULFITE,
            [Display(Name = "Treenut")]
            TREENUT,
            [Display(Name = "Wheat")]
            WHEAT
        }

        public enum Diet
        {
            [Display(Name = "Gluten free")]
            GLUTENFREE,
            [Display(Name = "Ketogenic")]
            KETOGENIC,
            [Display(Name = "Vegetarian")]
            VEGETARIAN,
            [Display(Name = "Lacto vegetarian")]
            LACTOVEGETARIAN,
            [Display(Name = "Ovo vegetarian")]
            OVOVEGETARIAN,
            [Display(Name = "Vegan")]
            VEGAN,
            [Display(Name = "Pescetarian")]
            PESCETARIAN,
            [Display(Name = "Paleo")]
            PALEO,
            [Display(Name = "Primal")]
            PRIMAL
        }

        public enum Gender
        {
            [Display(Name = "Male")]
            MALE,
            [Display(Name = "Female")]
            FEMALE
        }

        public enum Role
        {
            [Display(Name = "Admin")]
            ADMIN,
            [Display(Name = "Employee")]
            EMPLOYEE,
            [Display(Name = "User")]
            USER
        }
        public enum ActivityLevel
        {
            [Display(Name = "No exercise")]
            NOEXERCISE,
            [Display(Name = "Light exercise")]
            LIGHT,
            [Display(Name = "Moderate exercise")]
            MODERATE,
            [Display(Name = "Hard exercise")]
            HARD,
            [Display(Name = "Very hard exercise")]
            VERYHARD
        }
        public enum Benefit
        {
            [Display(Name = "Fat loss")]
            FATLOSS,
            [Display(Name = "Muscle gain")]
            MUSCLEGAIN,
            [Display(Name = "Stress reduction")]
            STRESSREDUCTION,
            [Display(Name = "Increased energy")]
            ENERGY,
            [Display(Name = "Improved sleep and mood")]
            MOOD,
            [Display(Name = "Develop healthy eating habits")]
            HEALTH
        }
    }
}
