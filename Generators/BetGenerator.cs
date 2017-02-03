using DiceGamingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiceGamingSystem.Generator
{
    public static class BetGenerator
    {
        public static Bet Generate ( Bet bet )
        {
            Random r = new Random();
            int firstDiceResult = r.Next(1 , 7);
            int secondDiceResult = r.Next(1 , 7);
            bet.RealResult = firstDiceResult + secondDiceResult;
            if(bet.ExpectedResult == bet.RealResult)
            {
                switch(bet.RealResult)
                {
                    case 2:
                    case 12:
                        bet.Win = (int)((double)bet.Stake * 4);
                        break;
                    case 3:
                    case 11:
                        bet.Win = (int)((double)bet.Stake * 3);
                        break;
                    case 4:
                    case 10:
                        bet.Win = (int)((double)bet.Stake * 2.5);
                        break;
                    case 5:
                    case 9:
                        bet.Win = (int)((double)bet.Stake * 2);
                        break;
                    case 6:
                    case 8:
                        bet.Win = (int)((double)bet.Stake * 1.5);
                        break;
                    case 7:
                        bet.Win = (int)((double)bet.Stake * 1);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                bet.Win = 0;
            }

            return bet;
        }
    }
}