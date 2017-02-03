using DiceGamingSystem.Models;
using DiceGamingSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DiceGamingSystem.Controllers
{
    public class SessionsController : ApiController
    {

        private readonly SessionsRepository sessionRepository;
        private readonly UsersRepository userRepository;

        public SessionsController ( SessionsRepository sRepo , UsersRepository uRepo )
        {
            this.sessionRepository = sRepo;
            this.userRepository = uRepo;
        }

        [HttpPost]
        [AllowAnonymous]
        public Task<HttpResponseMessage> Create ( [FromBody] Session session )
        {
            if(userRepository.Get(session.User.Id) != null
                && userRepository.Get(session.User.Id).Password==session.User.Password
                && userRepository.Get(session.User.Id).Username==session.User.Username)
            {
                sessionRepository.Create(session);
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.Created));
            }
            return Task.FromResult(Request.CreateResponse(HttpStatusCode.NotFound));
        }

        [HttpDelete]
        [AllowAnonymous]
        public Task<HttpResponseMessage> Delete ( int id )
        {
            sessionRepository.Delete(id);
            return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK));
        }
    }
}