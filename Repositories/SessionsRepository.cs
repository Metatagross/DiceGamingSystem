using DiceGamingSystem.Exceptions;
using DiceGamingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiceGamingSystem.Repository
{
    public class SessionsRepository
    {
        internal static Dictionary<int , Session> Sessions = new Dictionary<int , Session>();
        
        public void Create ( Session session )
        {
            if(Sessions.ContainsKey(session.Id))
                throw new BadRequestExcepiton("Session already exist"); 

            Sessions[session.Id] = session;
        }

        public void Delete ( int id )
        {
            if(!Sessions.ContainsKey(id))
                throw new NotFoundException("Session not found");

            Sessions.Remove(id);
        }

    }
}