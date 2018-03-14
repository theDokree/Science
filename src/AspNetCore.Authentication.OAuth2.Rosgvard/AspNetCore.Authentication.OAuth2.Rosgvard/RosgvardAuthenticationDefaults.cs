using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace AspNetCore.Authentication.OAuth2.Rosgvard
{
    /// <summary>
    /// Default values used by the Rosgvard authentication middleware.
    /// </summary>
    public static class RosgvardAuthenticationDefaults
    {
        /// <summary>
        /// Default value for <see cref="AuthenticationScheme.Name"/>.
        /// </summary>
        public const string AuthenticationScheme = "Rosgvard";

        /// <summary>
        /// Default value for <see cref="AuthenticationScheme.DisplayName"/>.
        /// </summary>
        public const string DisplayName = "Rosgvard";

        /// <summary>
        /// Default value for <see cref="AuthenticationSchemeOptions.ClaimsIssuer"/>.
        /// </summary>
        public const string Issuer = "Rosgvard";

        /// <summary>
        /// Default value for <see cref="RemoteAuthenticationOptions.CallbackPath"/>.
        /// </summary>
        public const string CallbackPath = "/signin-rosgvard";

        /// <summary>
        /// Default value for <see cref="OAuthOptions.AuthorizationEndpoint"/>.
        /// </summary>
        public const string AuthorizationEndpoint = "http://oauth.vv.mvd/oauth/authorize";
        //public const string AuthorizationEndpoint = "http://192.168.128.249:14717/oauth/authorize";
        /// <summary>
        /// Default value for <see cref="OAuthOptions.TokenEndpoint"/>.
        /// </summary>
        public const string TokenEndpoint = "http://oauth.vv.mvd/oauth/token";
        //public const string TokenEndpoint = "http://192.168.128.249:14717/oauth/token";
        /// <summary>
        /// Default value for <see cref="OAuthOptions.UserInformationEndpoint"/>.
        /// </summary>
        public const string UserInformationEndpoint = "http://api.vv.mvd/api/me";
        //public const string UserInformationEndpoint = "http://192.168.128.249:14717/api/me";
    }
}
