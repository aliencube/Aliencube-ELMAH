using Elmah;
using System;
using System.Web;
using System.Web.Mvc;

namespace Aliencube.Elmah.Mvc.Attributes
{
    /// <summary>
    /// This represents the error handling attribute entity with ELMAH.
    /// </summary>
    /// <remarks>
    /// http://stackoverflow.com/questions/766610/how-to-get-elmah-to-work-with-asp-net-mvc-handleerror-attribute
    /// </remarks>
    public class ElmahHandleErrorAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// Called when an exception occurs.
        /// </summary>
        /// <param name="filterContext">The action-filter context.</param>
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

        /// <summary>
        /// Raises the error signal so that ELMAH can handle it.
        /// </summary>
        /// <param name="ex">Exception instance.</param>
        /// <returns>Returns <c>True</c>, if signal is raised; otherwise returns <c>False</c>.</returns>
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

        /// <summary>
        /// Checks whether the exception is filtered or not.
        /// </summary>
        /// <param name="filterContext">The action-filter context.</param>
        /// <returns>Returns <c>True</c>, if the exception is filtered; otherwise returns <c>False</c>.</returns>
        private static bool IsFiltered(ExceptionContext filterContext)
        {
            var config = filterContext.HttpContext.GetSection("elmah/errorFilter") as ErrorFilterConfiguration;
            if (config == null)
                return false;

            var testContext = new ErrorFilterModule.AssertionHelperContext(filterContext.Exception, HttpContext.Current);

            return config.Assertion.Test(testContext);
        }

        /// <summary>
        /// Logs the exception details through ELMAH.
        /// </summary>
        /// <param name="ex">Exception instance.</param>
        private static void LogException(Exception ex)
        {
            var context = HttpContext.Current;
            ErrorLog.GetDefault(context).Log(new Error(ex, context));
        }
    }
}