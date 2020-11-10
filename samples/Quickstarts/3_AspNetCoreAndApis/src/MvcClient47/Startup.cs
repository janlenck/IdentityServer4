using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System.Collections.Generic;
using System.IdentityModel.Tokens;

[assembly: OwinStartup(typeof(MvcClient47.Startup))]

namespace MvcClient47
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = "https://localhost:5001/",
                RedirectUri = "https://localhost:44343/",
                ClientId = "mvc.owin",
                ClientSecret = "secret",
                ResponseType = "code id_token",
                Scope = "openid profile api1",

                UseTokenLifetime = false,
                SignInAsAuthenticationType = "Cookies",

                SaveTokens = true,
                RedeemCode = true
            });
        }
    }
}