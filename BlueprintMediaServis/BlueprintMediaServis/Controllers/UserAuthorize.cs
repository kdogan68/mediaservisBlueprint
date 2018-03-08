using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueprintMediaServis.Controllers
{
    public class UserAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["authenticationResult"] != null)
            {
                return true;
            }
            else
            {
                httpContext.Response.Redirect("/");
                return false;
            }

        }
    }
}