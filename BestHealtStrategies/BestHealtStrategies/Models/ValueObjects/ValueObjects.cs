using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestHealtStrategies.Models.ValueObjects
{
    public class ValueObjects
    {
        public enum Intolerance
        {
            DAIRY,
            EGG,
            GLUTEN,
            GRAIN,
            PEANUT,
            SEAFOOD,
            SESAME,
            SHELLFISH,
            SOY,
            SULFITE,
            TREENUT,
            WHEAT
        }

        public enum Diet
        {
            GLUTENFREE,
            KETOGENIC,
            VEGETARIAN,
            LACTOVEGETARIAN,
            OVOVEGETARIAN,
            VEGAN,
            PESCETARIAN,
            PALEO,
            PRIMAL
        }

        public enum Gender
        {
            MALE,
            FEMALE
        }

        public enum Role
        {
            ADMIN,
            EMPLOYEE,
            USER
        }
        public enum ActivityLevel
        {
            NOEXERCISE,
            LIGHT,
            MODERATE,
            HARD,
            VERYHARD
        }
        public enum Benefit
        {
            FATLOSS,
            MUSCLEGAIN,
            STRESSREDUCTION,
            ENERGY,
            MOOD,
            HEALTH
        }
    }
}
