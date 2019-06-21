using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;

namespace TestingExtensions
{
    public static class ApiControllerExtensions
    {
        public static void MockCurrentUserApi(this ApiController controller, string userId, string username)
        {
            var identity = new GenericIdentity(username);
            identity.AddClaim(
               new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userId));
            identity.AddClaim(
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", username));

            var principal = new GenericPrincipal(identity, null);

            controller.User = principal;
        }
    }
}
