﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace DiceGamingSystem.Filters
{
    public class DefaultExceptionFilterAttribute: ExceptionFilterAttribute
    {
        public override void OnException ( HttpActionExecutedContext actionExecutedContext )
        {
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError ,
                    actionExecutedContext.Exception.Message);

            base.OnException(actionExecutedContext);
        }
    }
}