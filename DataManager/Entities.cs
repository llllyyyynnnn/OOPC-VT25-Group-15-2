using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataManager
{
    public class Entity
    {
        [Required] [Key] public int id { get; set; }
    }

    public class Entities
    {
        public class Member : Entity
        {
            [Required] [MaxLength(64)] public required string firstName { get; set; }
            [Required] [MaxLength(64)] public required string lastName { get; set; }
            [Required] [MaxLength(128)] public required string mailAddress { get; set; }
            [Required] [MaxLength(24)] public required string phoneNumber { get; set; }
            [Required] public required DateTime birthDate { get; set; }
            [Required] [MaxLength(100)] public required string pinCode { get; set; }
        }

        public class Coach : Entity
        {
            [Required][MaxLength(64)] public required string firstName { get; set; }
            [Required][MaxLength(64)] public required string lastName { get; set; }
            [Required][MaxLength(128)] public required string mailAddress { get; set; }
            [Required][MaxLength(24)] public required string phoneNumber { get; set; }
            [Required] public required DateTime birthDate { get; set; }
            [Required][MaxLength(100)] public required string pinCode { get; set; }
            public required string specialisation { get; set; }
        }

        public class Session : Entity
        {
            [Required][MaxLength(64)] public required string name { get; set; }
            [Required] public required DateTime startTime { get; set; }
            [Required] public required DateTime endTime { get; set; }
            [Required][MaxLength(64)] public required string location { get; set; }
            [Required] public required Member member { get; set; }
            [Required] public required Coach coach { get; set; }
        }

        public class Category : Entity
        {
            [Required][MaxLength(64)] public required string name { get; set; }
        }

        public class Gear : Entity
        {
            [Required][MaxLength(64)] public required string name { get; set; }
            [Required] public required Category category { get; set; }
            [Required][MaxLength(64)] public required string condition { get; set; }
            [Required] public required bool available { get; set; }
        }

        public class GearLoan : Entity
        {
            [Required] public required Gear gear { get; set; }
            [Required] public required Member loanOwner { get; set; }
            [Required] public required DateTime loanDate { get; set; }
            [Required] public required DateTime returnDate { get; set; }
        }
    }
}
