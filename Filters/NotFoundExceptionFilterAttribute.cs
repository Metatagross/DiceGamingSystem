using DiceGamingSystem.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace DiceGamingSystem.Filters
{
    public class NotFoundExceptionFilterAttribute: ExceptionFilterAttribute
    {
        public override void OnException ( HttpActionExecutedContext actionExecutedContext )
        {
            if(actionExecutedContext.Exception is NotFoundException)
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.NotFound ,
                    actionExecutedContext.Exception.Message);

            base.OnException(actionExecutedContext);
        }
    }
}