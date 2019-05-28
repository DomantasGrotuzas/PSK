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
            var username = context.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
            var userRole = string.Join(", ", context.HttpContext?.User?.Claims?.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"));

            var logText =
                $"{DateTime.Now} - {nameof(username)}:'{username}'; {nameof(userRole)}:'{userRole}'; method:{controllerName}.{actionName}";


            using (var stream = _logFile.AppendText())
            {
                stream.WriteLine(logText);
            }
        }
    }
}