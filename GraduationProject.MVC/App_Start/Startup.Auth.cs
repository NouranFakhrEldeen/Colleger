using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Owin;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security.Cookies;

namespace GraduationProject.MVC
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                SlidingExpiration = true,
                ExpireTimeSpan = new TimeSpan(0, 300, 0),
                CookieName = "GraduationProject",
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnResponseSignedIn = ctx =>
                    {
                        ctx.Options.SlidingExpiration = true;
                        ctx.Options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    },
                    OnApplyRedirect = ctx =>
                    {
                        if (IsAjaxRequest(ctx.Request))
                            return;
                        ctx.Response.Redirect(HttpUtility.HtmlDecode($"{ctx.RedirectUri}"));
                    },
                    OnValidateIdentity = MyCustomValidateIdentity
                }
            });
        }


        private static bool IsAjaxRequest(IOwinRequest request)
        {
            var headers = request.Headers;
            return ((headers != null) && (headers["X-Requested-With"] == "XMLHttpRequest"));
        }

        private static Task MyCustomValidateIdentity(CookieValidateIdentityContext context)
        {

            var expireUtc = context.Properties.ExpiresUtc;

            var claimType = "myExpireUtc";
            var identity = context.Identity;
            if (identity.HasClaim(c => c.Type == claimType))
            {
                var existingClaim = identity.FindFirst(claimType);
                identity.RemoveClaim(existingClaim);
            }
            if (expireUtc != null)
            {
                var newClaim = new Claim(claimType, expireUtc.Value.UtcTicks.ToString());
                context.Identity.AddClaim(newClaim);
            }

            return Task.FromResult(0);
        }



    }
}