using System;
using BrregAPI.Modals;
using Hangfire.Dashboard;

namespace BrregAPI.Handlers {
public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();
        var _context = (BrregContext)httpContext.RequestServices.GetService(typeof(BrregContext));

        var user = _context.Users.FirstOrDefault(x=> x.UserName == httpContext.User.Identity.Name);
        if (user == null) return false;
        
        return user.IsAdmin;
    }
}
}
