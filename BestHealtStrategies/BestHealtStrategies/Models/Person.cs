using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static BestHealtStrategies.Models.ValueObjects.ValueObjects;

namespace BestHealtStrategies.Models
{
    public abstract class Person: IdentityUser
    {
        protected Person() { }
        protected Person(string name, string surname, Role role)
        {
            Name = name;
            Surname = surname;
            Role = role;
        }
        protected Person(string name, string surname, Role role, string email)
        {
            Name = name;
            Surname = surname;
            Role = role;
            Email = email;
            NormalizedEmail = email.ToUpper();
        }
        [Required, RegularExpression(@"[a-zA-Z ]+")]
        public string Name { get; set; }
        [Required, RegularExpression(@"[a-zA-Z ]+")]
        public string Surname { get; set; }
        [EnumDataType(typeof(Role)), ScaffoldColumn(false)]
        public Role Role { get; set; }
    }
}
