using RealEstate.Core;
using System.Security.Claims;

#nullable enable
namespace RealEstate.Api
{
    public class RequestContextProvider(IHttpContextAccessor contextAccessor): IRequestContextProvider
    {
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
        //public bool IsAdmin()
        //{

        //}
        //public bool IsUser()
        //{

        //}

        //public string? GetUserId()
        //{

        //}
        public string? GetUserName()
        {
            return _contextAccessor?.HttpContext?.User.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
        }
        public string? GetUserEmail()
        {
            return _contextAccessor?.HttpContext?.User.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
        }
    }
}
