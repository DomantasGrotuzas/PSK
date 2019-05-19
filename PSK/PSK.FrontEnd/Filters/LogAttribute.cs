using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PSK.FrontEnd.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
        private readonly FileInfo _logFile;

        public LogAttribute()
        {
            _logFile = new FileInfo("log.txt");
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LogMethod(filterContext);
        }

        public void LogMethod(ActionExecutingContext context)
        {
            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];

            //TODO: will probably be changed regarding how the login is implemented
            var username = context.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "username")?.Value;
            var userRole = context.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "role")?.Value;

            var logText =
                $"{DateTime.Now} - {nameof(username)}:'{username}'; {nameof(userRole)}:'{userRole}'; method:{controllerName}.{actionName}";


            using (var stream = _logFile.AppendText())
            {
                stream.WriteLine(logText);
            }
        }
    }
}