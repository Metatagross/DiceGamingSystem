using DiceGamingSystem.Enums;
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
    public class UsersController : ApiController
    {
        private readonly UsersRepository repository;

        public UsersController ( UsersRepository repository )
        {
            this.repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public Task<HttpResponseMessage> Get ( int id )
        {
            var user = repository.Get(id);
            return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK , new { Id=user.Id, Username=user.Username, Fullname=user.FullName, Email=user.Email }));
        }

        [HttpPost]
        [AllowAnonymous]
        public Task<HttpResponseMessage> Create ( [FromBody] User user )
        {
            repository.Create(user);
            return Task.FromResult(Request.CreateResponse(HttpStatusCode.Created));
        }

        [HttpPut]
        [AllowAnonymous]
        public Task<HttpResponseMessage> Update ( [FromBody] User user )
        {
            repository.Update(user);
            return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK));
        }

        [HttpPost]
        [Route("api/users/{id}/bets")]
        [AllowAnonymous]
        public Task<HttpResponseMessage> CreateBet ( int id , [FromBody] Bet bet )
        {
            repository.CreateBet(id , bet);
            return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK));
        }

        [HttpDelete]
        [AllowAnonymous]
        public Task<HttpResponseMessage> Delete ( int id , string password)
        {
            repository.Delete(id, password);
            return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK));
        }

        [HttpPut]
        [Route("api/users/{id}/ballance")]
        [AllowAnonymous]
        public Task<HttpResponseMessage> ChargeBallance(int id, int amount )
        {
            int currentBalance=repository.Charge(id , amount);
            return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK , currentBalance));
        }

        [HttpDelete]
        [Route("api/users/{userId}/bets/{betId}")]
        [AllowAnonymous]
        public Task<HttpResponseMessage> CancelBet(int userId, int betId )
        {
            repository.CancelBet(userId , DateTime.Now , repository.Get(userId , betId));
            return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK));
        }

        [HttpGet]
        [Route("api/users/{userId}/bets/{betId}")]
        [AllowAnonymous]
        public Task<HttpResponseMessage> GetBet(int userId, int betId )
        {
            Bet bet = repository.Get(userId , betId);
            return Task.FromResult(Request.CreateResponse(HttpStatusCode.Found , bet));
        }
        [HttpGet]
        [Route("api/users/{id}/bets")]
        [AllowAnonymous]
        public Task<HttpResponseMessage> GetBets(int id,int page, int size, BetTypes type )
        {
            var bets = repository.GetBets(id , page , size , type);
            return Task.FromResult(Request.CreateResponse(HttpStatusCode.Found , bets));
        }
    }
}