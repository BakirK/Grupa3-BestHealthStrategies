using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BestHealtStrategies.Models.ValueObjects.ValueObjects;

namespace BestHealtStrategies.Models
{
    public class Administrator : Person
    {
        public Administrator() { }
        public Administrator(string name, string surname) : 
            base(name, surname, Role.ADMIN)
        {
        }
    }
}
