using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiceGamingSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Balance { get; set; }
        public List<Bet> Bets { get; set; }

    }
}