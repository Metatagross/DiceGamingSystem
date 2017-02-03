using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiceGamingSystem.Models
{
    public class Bet
    {
        public int Id { get; set; }
        public int ExpectedResult { get; set; }
        public int Stake { get; set; }
        public int RealResult { get; set; }
        public int Win { get; set; }
        public DateTime Timestamp { get; set; }
    }
}