using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataManager
{
    public class Entity
    {
        [Key] public int id { get; set; }
    }

    public class Entities
    {
        public class Member : Entity
        {
            public required string firstName { get; set; }
            public required string lastName { get; set; }
            public required string mailAddress { get; set; }
            public required string phoneNumber { get; set; }
            public required DateOnly birthDate { get; set; }
            public required string pinCode { get; set; }
        }

        public class Coach : Entity
        {
            public required string firstName { get; set; }
            public required string lastName { get; set; }
            public required string mailAddress { get; set; }
            public required string phoneNumber { get; set; }
            public required string specialisation { get; set; }
            public required DateTime birthDate { get; set; }
            public required string pinCode { get; set; }
        }

        public class Session : Entity
        {
            public required string name { get; set; }
            public required DateTime startTime { get; set; }
            public required DateTime endTime { get; set; }
            public required string location { get; set; }
            public required Member member { get; set; }
            public required Coach coach { get; set; }
        }

        public class Category : Entity
        {
            public required string name { get; set; }
        }

        public class Gear : Entity
        {
            public required string name { get; set; }
            public required Category category { get; set; }
            public required string condition { get; set; }
            public required bool available { get; set; }
        }

        public class GearLoan : Entity
        {
            public required Gear gear { get; set; }
            public required Member loanOwner { get; set; }
            public required DateTime loanDate { get; set; }
            public required DateTime returnDate { get; set; }
        }
    }
}
