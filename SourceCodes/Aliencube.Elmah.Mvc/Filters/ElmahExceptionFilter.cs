using Elmah;
using System.Web.Mvc;

namespace Aliencube.Elmah.Mvc.Filters
{
    /// <summary>
    /// This represents the error filtering entity with ELMAH.
    /// </summary>
    /// <remarks>
    /// http://ivanz.com/2011/05/08/asp-net-mvc-magical-error-logging-with-elmah/
    /// </remarks>
    public class ElmahExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Called when an exception occurs.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnException(ExceptionContext filterContext)
        {
            // Long only handled exceptions, because all other will be caught by ELMAH anyway.
            if (filterContext.ExceptionHandled)
                ErrorSignal.FromCurrentContext().Raise(filterContext.Exception);
        }
    }
}