using BookSiteProject.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Infrastructure.Extensions
{
    public class ForcePasswordChangeMiddleware
    {
        private readonly RequestDelegate _next;

        public ForcePasswordChangeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(context.User);
                if (user != null && user.MustChangePassword)
                {
                    if (!context.Request.Path.Value.Equals("/Identity/Account/Manage/ChangePassword", StringComparison.OrdinalIgnoreCase)
                        && !context.Request.Path.Value.Equals("/Identity/Account/Logout", StringComparison.OrdinalIgnoreCase))
                    {
                        context.Response.Redirect("/Identity/Account/Manage/ChangePassword");
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
