using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TrackHub.Web.Controllers;

public abstract class TrackHubController : Controller
{
    public string CurrentUserId 
    {
        get
        {
            return User.Claims.First(claim => claim.Type! == ClaimTypes.Sid).Value;
        }        
    }
}
