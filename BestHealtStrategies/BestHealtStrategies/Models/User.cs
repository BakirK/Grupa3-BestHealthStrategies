using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static BestHealtStrategies.Models.ValueObjects.ValueObjects;

namespace BestHealtStrategies.Models
{
    public class User : Person
    {
        public User(int iD, string email, string password, string name, string surname) : 
            base(iD, email, password, name, surname, Role.USER)
        {
        }

        public User(int iD, string email, string password, string name, string surname, 
            int age, int height, int weight, Gender gender, ActivityLevel activity, 
            Benefit benefit, Diet diet, List<Intolerance> intolerances, 
            List<ProgressHistory> progressHistroy, List<DailyMealPlan> weeklyMealPlan, int personId) : 
            base(iD, email, password, name, surname, Role.USER)
        {
            Age = age;
            Height = height;
            Weight = weight;
            Gender = gender;
            Activity = activity;
            Benefit = benefit;
            Diet = diet;
            Bmi = Weight / Height * Height;
            Intolerances = intolerances;
            TargetCalories = CalculateTargetCalories();
            ProgressHistroy = progressHistroy;
            WeeklyMealPlan = weeklyMealPlan;
            PersonId = personId;
        }
        [Required]
        public int Age { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required, EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [Required, EnumDataType(typeof(ActivityLevel))]
        public ActivityLevel Activity { get; set; }

        [Required, EnumDataType(typeof(Benefit))]
        public Benefit Benefit { get; set; }

        [Required, EnumDataType(typeof(Diet))]
        public Diet Diet { get; set; }

        [HiddenInput(DisplayValue = false)]
        public double Bmi { get; set; }

        public List<Intolerance> Intolerances { get; set; }

        [HiddenInput(DisplayValue = false)]
        public double TargetCalories { get; set; }

        [InverseProperty("User")]
        public List<ProgressHistory> ProgressHistroy { get; set; }

        [InverseProperty("User")]
        public List<DailyMealPlan> WeeklyMealPlan { get; set; }
        [InverseProperty("User")]
        public List<Rating> Raitings { get; set; }
        [HiddenInput(DisplayValue = false), ForeignKey("Person")]
        public int PersonId { get; set; }

        private double CalculateTargetCalories()
        {
            double basalMetabolicRate;

            if (Gender == Gender.MALE)
                basalMetabolicRate = 88.362 + (13.397 * Weight) + (4.799 * Height) - (5.677 * Age);
            else
                basalMetabolicRate = 447.593 + (9.247 * Weight) + (3.098 * Height) - (4.330 * Age);

            switch (Activity)
            {
                case ActivityLevel.NOEXERCISE:
                    {
                        basalMetabolicRate *= 1.2;
                        break;
                    }
                case ActivityLevel.LIGHT:
                    {
                        basalMetabolicRate *= 1.375;
                        break;
                    }
                case ActivityLevel.MODERATE:
                    {
                        basalMetabolicRate *= 1.55;
                        break;
                    }
                case ActivityLevel.HARD:
                    {
                        basalMetabolicRate *= 1.725;
                        break;
                    }
                case ActivityLevel.VERYHARD:
                    {
                        basalMetabolicRate *= 1.9;
                        break;
                    }
            }

            // Severely underweight 
            if (Bmi < 16)
                basalMetabolicRate += 400;
            // Underweight
            else if (Bmi > 16 && Bmi < 18.5)
                basalMetabolicRate += 200;
            // Overweight
            else if (Bmi > 25 && Bmi < 30)
                basalMetabolicRate -= 100;
            // Obese Class I
            else if (Bmi > 30 && Bmi < 35)
                basalMetabolicRate -= 200;
            // Obese Class II
            else if (Bmi > 35 && Bmi < 40)
                basalMetabolicRate -= 300;
            // Obese Class III
            else
                basalMetabolicRate -= 400;

            return basalMetabolicRate;
        }

    }
}
