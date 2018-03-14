using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;

namespace AspNetCore.Authentication.OAuth2.Rosgvard
{
    /// <summary>
    /// Defines a set of options used by <see cref="RosgvardAuthenticationHandler"/>.
    /// </summary>
    public class RosgvardAuthenticationOptions : OAuthOptions
    {
        public RosgvardAuthenticationOptions()
        {
            ClaimsIssuer = RosgvardAuthenticationDefaults.Issuer;
            CallbackPath = new PathString(RosgvardAuthenticationDefaults.CallbackPath);

            AuthorizationEndpoint = RosgvardAuthenticationDefaults.AuthorizationEndpoint;
            TokenEndpoint = RosgvardAuthenticationDefaults.TokenEndpoint;
            UserInformationEndpoint = RosgvardAuthenticationDefaults.UserInformationEndpoint;

            ClaimActions.MapJsonKey("displayName", "displayName");
            ClaimActions.MapJsonKey("company", "company");
            ClaimActions.MapJsonKey("domain", "domain");
            ClaimActions.MapCustomJson(ClaimTypes.NameIdentifier, user => user.Value<string>("id"));
            ClaimActions.MapCustomJson(ClaimTypes.Email, user => user.Value<string>("emailAddress"));
        }
    }
}
