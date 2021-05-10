using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static BestHealtStrategies.Models.ValueObjects.ValueObjects;

namespace BestHealtStrategies.Models
{
    public class Employee : Person
    {
        public Employee(int iD, string email, string password, string name, string surname) :
            base(iD, email, password, name, surname, Role.EMPLOYEE)
        {
        }
        [Required]
        public string Description { get; set; }
    }
}
