using System.Web.Mvc;

namespace Ninesky.Web.Areas.Member
{
    public class MemberAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Member";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            
            context.MapRoute(
                name: "Member_default",
                url: "Member/{controller}/{action}/{id}",
                defaults:new
                {
                    action = "Index",
                    id = UrlParameter.Optional
                },
                namespaces:new string[] { "Ninesky.Web.Areas.Member.Controllers" }
            );
        }
    }
}