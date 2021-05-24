using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static BestHealtStrategies.Models.ValueObjects.ValueObjects;

namespace BestHealtStrategies.Models
{
    public abstract class Person
    {
        protected Person() { }
        protected Person(int iD, string email, string password, string name, string surname, Role role)
        {
            ID = iD;
            Email = email;
            Password = password;
            Name = name;
            Surname = surname;
            Role = role;
        }
        [Key]
        public int ID { get; set; }
        [EmailAddress, Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required, RegularExpression(@"[a-zA-Z ]+")]
        public string Name { get; set; }
        [Required, RegularExpression(@"[a-zA-Z ]+")]
        public string Surname { get; set; }
        [EnumDataType(typeof(Role)), ScaffoldColumn(false)]
        public Role Role { get; set; }
    }
}
