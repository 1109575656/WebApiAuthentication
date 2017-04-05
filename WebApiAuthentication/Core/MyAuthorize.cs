using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebApiAuthentication.Core
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        protected virtual void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,"未授权");
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            if (!actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any<AllowAnonymousAttribute>())
            {
                return actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any<AllowAnonymousAttribute>();
            }
            return true;
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!SkipAuthorization(actionContext) && !this.IsAuthorized(actionContext))
            {
                this.HandleUnauthorizedRequest(actionContext);
            }
        }
        protected virtual bool IsAuthorized(HttpActionContext actionContext)
        {
            IPrincipal principal = actionContext.ControllerContext.RequestContext.Principal;
            if (((principal == null) || (principal.Identity == null)) || !principal.Identity.IsAuthenticated)
            {
                return false;
            }
            return true;
        }

    }
}