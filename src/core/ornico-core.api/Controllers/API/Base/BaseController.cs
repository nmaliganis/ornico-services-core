using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ornico.core.api.Controllers.API.Base
{
  public abstract class BaseController : ControllerBase
  {
    protected string GetEmailFromClaims()
    {
      var claimsPrincipal = User as ClaimsPrincipal;
      var email = claimsPrincipal?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")
        .Value;
      return email;
    }

    protected Guid GetUserIdFromClaims()
    {
      var claimsPrincipal = User as ClaimsPrincipal;
      return Guid.Parse(claimsPrincipal?.Claims.FirstOrDefault(x=>x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata")
        .Value);
    }
  }
}