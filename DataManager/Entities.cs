using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataManager
{
    public class Entity
    {
        [Key] public required int id { get; set; }
    }

    public class Entities
    {
        public class Individual : Entity
        {
            public required string fullName { get; set; }
        }

        public class Member : Individual
        {
            public required DateOnly birthDate { get; set; }
            public required string phoneNumber { get; set; }
            public required string mailAddress { get; set; }
        }

        public class Coach : Individual
        {
            public required Individual individual { get; set; }
            public required string specialisation { get; set; }
            public required string temporaryPin { get; set; } // before adding passcodes, implement some kind of encryption
        }

        public class Session : Entity
        {
            public required string name { get; set; }
            public required DateTime startTime, endTime;
            public required string Location { get; set; }
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
            public required DateOnly loanDate, returnDate;
        }
    }
}
