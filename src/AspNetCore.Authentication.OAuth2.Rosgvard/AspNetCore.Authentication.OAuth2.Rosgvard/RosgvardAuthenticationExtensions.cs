using System;
using Microsoft.AspNetCore.Authentication;
using AspNetCore.Authentication.OAuth2.Rosgvard;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods to add Rosgvard authentication capabilities to an HTTP application pipeline.
    /// </summary>
    public static class RosgvardAuthenticationExtensions
    {
        /// <summary>
        /// Adds the <see cref="RosgvardAuthenticationHandler"/> to the specified
        /// <see cref="AuthenticationBuilder"/>, which enables Rosgvard authentication capabilities.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static AuthenticationBuilder AddRosgvard(/*[NotNull]*/ this AuthenticationBuilder builder)
        {
            return builder.AddRosgvard(RosgvardAuthenticationDefaults.AuthenticationScheme, options => { });
        }

        /// <summary>
        /// Adds the <see cref="RosgvardAuthenticationHandler"/> to the specified
        /// <see cref="AuthenticationBuilder"/>, which enables Rosgvard authentication capabilities.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="configuration">The delegate used to configure the OpenID 2.0 options.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static AuthenticationBuilder AddRosgvard(
            /*[NotNull]*/ this AuthenticationBuilder builder,
            /*[NotNull]*/ Action<RosgvardAuthenticationOptions> configuration)
        {
            return builder.AddRosgvard(RosgvardAuthenticationDefaults.AuthenticationScheme, configuration);
        }

        /// <summary>
        /// Adds <see cref="RosgvardAuthenticationHandler"/> to the specified
        /// <see cref="AuthenticationBuilder"/>, which enables Rosgvard authentication capabilities.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="scheme">The authentication scheme associated with this instance.</param>
        /// <param name="configuration">The delegate used to configure the Rosgvard options.</param>
        /// <returns>The <see cref="AuthenticationBuilder"/>.</returns>
        public static AuthenticationBuilder AddRosgvard(
            /*[NotNull]*/ this AuthenticationBuilder builder, /*[NotNull]*/ string scheme,
            /*[NotNull]*/ Action<RosgvardAuthenticationOptions> configuration)
        {
            return builder.AddRosgvard(scheme, RosgvardAuthenticationDefaults.DisplayName, configuration);
        }

        /// <summary>
        /// Adds <see cref="RosgvardAuthenticationHandler"/> to the specified
        /// <see cref="AuthenticationBuilder"/>, which enables Rosgvard authentication capabilities.
        /// </summary>
        /// <param name="builder">The authentication builder.</param>
        /// <param name="scheme">The authentication scheme associated with this instance.</param>
        /// <param name="name">The optional display name associated with this instance.</param>
        /// <param name="configuration">The delegate used to configure the Rosgvard options.</param>
        /// <returns>The <see cref="AuthenticationBuilder"/>.</returns>
        public static AuthenticationBuilder AddRosgvard(
            /*[NotNull]*/ this AuthenticationBuilder builder,
            /*[NotNull]*/ string scheme, /*[CanBeNull]*/ string name,
            /*[NotNull]*/ Action<RosgvardAuthenticationOptions> configuration)
        {
            return builder.AddOAuth<RosgvardAuthenticationOptions, RosgvardAuthenticationHandler>(scheme, name, configuration);
        }
    }
}
