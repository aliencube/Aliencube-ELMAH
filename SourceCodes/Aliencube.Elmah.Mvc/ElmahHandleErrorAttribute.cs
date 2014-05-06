using Elmah;
using System;
using System.Web;
using System.Web.Mvc;

namespace Aliencube.Elmah.Mvc
{
    /// <summary>
    /// This represents the error handling attribute entity with ELMAH.
    /// </summary>
    /// <remarks>
    /// http://stackoverflow.com/questions/766610/how-to-get-elmah-to-work-with-asp-net-mvc-handleerror-attribute
    /// </remarks>
    public class ElmahHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            var ex = filterContext.Exception;
            if (!filterContext.ExceptionHandled // if unhandled, will be logged anyhow
                || RaiseErrorSignal(ex)         // prefer signaling, if possible
                || IsFiltered(filterContext))   // filtered?
            {
                return;
            }

            LogException(ex);
        }

        private static bool RaiseErrorSignal(Exception ex)
        {
            var context = HttpContext.Current;
            if (context == null)
                return false;

            var signal = ErrorSignal.FromContext(context);
            if (signal == null)
                return false;

            signal.Raise(ex, context);
            return true;
        }

        private static bool IsFiltered(ExceptionContext filterContext)
        {
            var config = filterContext.HttpContext.GetSection("elmah/errorFilter") as ErrorFilterConfiguration;
            if (config == null)
                return false;

            var testContext = new ErrorFilterModule.AssertionHelperContext(filterContext.Exception, HttpContext.Current);

            return config.Assertion.Test(testContext);
        }

        private static void LogException(Exception ex)
        {
            var context = HttpContext.Current;
            ErrorLog.GetDefault(context).Log(new Error(ex, context));
        }
    }
}