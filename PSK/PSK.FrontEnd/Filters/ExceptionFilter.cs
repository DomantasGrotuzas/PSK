using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace PSK.FrontEnd.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is DbUpdateConcurrencyException)
            {
                context.HttpContext.Response.StatusCode = 409;
                context.Result = new ConflictResult();
            }
            else
            {
                context.HttpContext.Response.StatusCode = 500;
            }

            base.OnException(context);
        }
    }
}