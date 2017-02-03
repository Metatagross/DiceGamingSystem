using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DiceGamingSystem.Filters
{
    public class UsersAuthorizationFilterAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization ( HttpActionContext actionContext )
        {
            base.OnAuthorization(actionContext);

            if(actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                       || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return;
            }
            else
            {
                IEnumerable<string> auths;
                var auth = string.Empty;
                if(actionContext.Request.Headers.TryGetValues("AuthorizationHeader" , out auths))
                    auth = auths.FirstOrDefault();

                if(auth != "Authorized")
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized , "No rights to access this resource");

            }
        }
    }
}