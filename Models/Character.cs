using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
    public class Character
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public float Age { get; set; } = 10f;
        public RpgClass Class { get; set; } = RpgClass.Hobbit;
        public bool IsDead {get; set;} = false;
        public User? User { get; set; }
    }
}