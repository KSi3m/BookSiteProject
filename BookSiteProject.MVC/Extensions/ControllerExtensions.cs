using BookSiteProject.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BookSiteProject.MVC.Extensions
{
    public static class ControllerExtensions
    {
        public static void SetNotifications(this Controller controller, NotificationType type, string message)
        {
            var notification = new Notification(type.ToString(), message);
            controller.TempData["notification"] = JsonConvert.SerializeObject(notification);
        }

    }
}
