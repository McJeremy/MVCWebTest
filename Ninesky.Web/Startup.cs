using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(Ninesky.Web.Startup))]
namespace Ninesky.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //app.CreatePerOwinContext(ApplicationDbContext.Create);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            //app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            //app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);


            //            app.UseCookieAuthentication(new CookieAuthenticationOptions
            //            {
            //                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //                LoginPath = new PathString("/Area/User/Login"),
            //                Provider = new CookieAuthenticationProvider
            //{
            //    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity < ApplicationUserManager， ApplicationUser > (
            //                        validateInterval: TimeSpan.FromMinutes(30)，
            //                        regenerateIdentity: (manager， user) => user.GenerateUserIdentityAsync(manager))
            //                }
            //    });
            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie， TimeSpan.FromMinutes(5));


            //app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie)
        }
    }
}
