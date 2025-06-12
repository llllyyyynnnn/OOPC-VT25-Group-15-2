using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager
{
    public class Entities
    {
        public class Member
        {
            [Required][Key] public int id { get; set; }
            [Required] [MaxLength(64)] public required string firstName { get; set; }
            [Required] [MaxLength(64)] public required string lastName { get; set; }
            [Required] [MaxLength(128)] public required string mailAddress { get; set; }
            [Required] [MaxLength(24)] public required string phoneNumber { get; set; }
            [Required] public required DateTime birthDate { get; set; }
            [Required] [MaxLength(100)] public required string pinCode { get; set; }
            public List<Entities.Session> sessions { get; set; } = null;
        }

        public class Coach
        {
            [Required][Key] public int id { get; set; }
            [Required][MaxLength(64)] public required string firstName { get; set; }
            [Required][MaxLength(64)] public required string lastName { get; set; }
            [Required][MaxLength(128)] public required string mailAddress { get; set; }
            [Required][MaxLength(24)] public required string phoneNumber { get; set; }
            [Required] public required DateTime birthDate { get; set; }
            [Required][MaxLength(100)] public required string pinCode { get; set; }
            public required string specialisation { get; set; }
            public List<Entities.Session> sessions { get; set; } = null;
        }

        public class Session
        {
            [Required][Key] public int id { get; set; }
            [Required][MaxLength(64)] public required string activity { get; set; }
            [Required][MaxLength(128)] public required string description { get; set; }
            [Required] public required int caloriesBurnt { get; set; }
            [Required] public required int participants { get; set; }
            [Required] public required DateTime date { get; set; }
            [Required] public required TimeOnly time { get; set; }
            [Required][MaxLength(64)] public required string location { get; set; }
            [Required] public int Coachid { get; set; }
            [ForeignKey("Coachid")] public required Coach coach { get; set; }
            public List<Entities.Member> members { get; set; }
        }

        public class Gear
        {
            [Required][Key] public int id { get; set; }
            [Required][MaxLength(64)] public required string name { get; set; }
            [Required] public required string category { get; set; }
            [Required][MaxLength(64)] public required string condition { get; set; }
            [Required] public required bool available { get; set; }
        }

        public class GearLoan
        {
            [Required][Key] public int id { get; set; }
            [Required] public required Gear gear { get; set; }
            [Required] public required Member loanOwner { get; set; }
            [Required] public required DateTime loanDate { get; set; }
            [Required] public required DateTime returnDate { get; set; }
        }
    }
}
