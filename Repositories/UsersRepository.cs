using DiceGamingSystem.Exceptions;
using DiceGamingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiceGamingSystem.Generator;
using DiceGamingSystem.Enums;

namespace DiceGamingSystem.Repository
{
    public class UsersRepository
    {

        internal static Dictionary<int , User> Users = new Dictionary<int , User>();

        public User Get ( int id )
        {
            if(!Users.ContainsKey(id))
                throw new NotFoundException("User not found!");

            User userModel = new User
            {
                Username = Users[id].Username ,
                Password = Users[id].Password ,
                FullName = Users[id].FullName ,
                Email = Users[id].Email
            };
            return userModel;
        }

        public void Create ( User user )
        {
            if(user.Id == 0)
            {
                user.Id = Users.Keys.Any() ? Users.Keys.Max() + 1 : 1;
            }
            if(Users.ContainsKey(user.Id))
                throw new BadRequestExcepiton("Id already exists");
            if(user.Password == null || user.Username == null)
                throw new BadRequestExcepiton("Username and password are required");
            Users[user.Id] = user;
        }

        public void Update ( User user )
        {
            if(!Users.ContainsKey(user.Id))
                throw new NotFoundException("User not found");
            if(Users[user.Id].Password != user.Password)
                throw new BadRequestExcepiton("Wrong password");
            if(Users[user.Id].Username != user.Username)
                throw new BadRequestExcepiton("You can't change your name");
            Users[user.Id] = user;
        }

        public void Delete ( int id , string password )
        {
            if(!Users.ContainsKey(id))
                throw new NotFoundException("User not found");
            if(Users[id].Password != password)
                throw new BadRequestExcepiton("Wrong password");
            Users.Remove(id);
        }

        public int Charge ( int id , int balance )
        {
            if(!Users.ContainsKey(id))
                throw new NotFoundException("User not found");
            Users[id].Balance += balance;
            return Users[id].Balance;
        }

        public int CheckBalance ( int id )
        {
            if(!Users.ContainsKey(id))
                throw new NotFoundException("User not found");

            return Users[id].Balance;
        }

        public Bet CreateBet ( int id , Bet bet )
        {
            
            if(!Users.ContainsKey(id))
                throw new NotFoundException("User not found");
            if(Users[id].Bets == null)
            {
                Users[id].Bets = new List<Bet>();
            }
            if(bet.Id == 0)
            {
                bet.Id = Users[id].Bets.Any() ? Users[id].Bets.Max(b=>b.Id) + 1 : 1;
            }
            bet = BetGenerator.Generate(bet);
            bet.Timestamp = DateTime.Now;
            Users[id].Bets.Add(bet);
            return new Bet { Id = bet.Id , Win = bet.Win };
        }

        public bool CancelBet ( int id , DateTime date , Bet bet )
        {

            if(!Users.ContainsKey(id))
                throw new NotFoundException("User not found");
            if((date - bet.Timestamp).TotalSeconds < 60)
            {
                Users[id].Bets.Remove(bet);
                return true;
            }
            else
            {
                throw new BadRequestExcepiton("Can not cancel the bet");
            }
        }

        public List<dynamic> GetBets ( int id , int page , int size , BetTypes type )
        {
            if(!Users.ContainsKey(id))
                throw new NotFoundException("User not found");

            return Users[id].Bets
              .Select(kvp => new { Id = kvp.Id , Stake = kvp.Stake , Win = kvp.Win , Timestamp = kvp.Timestamp })
              .OrderBy(s => s.Id)
              .Where(kvp =>
              {
                  switch(type)
                  {
                      case BetTypes.All:
                          return kvp.Win >= 0;
                      case BetTypes.Lost:
                          return kvp.Win == 0;
                      case BetTypes.Winning:
                          return kvp.Win > 0;
                      default:
                          return true;
                  }
              })
              .Skip((page - 1) * size)
              .Take(size)
              .ToList<dynamic>();
        }
        public Bet Get ( int userId , int betId )
        {
            if(!Users.ContainsKey(userId) || Users[userId].Bets.Find(b => b.Id == betId) == null)
                throw new NotFoundException("Content not found");

            return Users[userId].Bets.Find(b => b.Id == betId);
        }
    }
}