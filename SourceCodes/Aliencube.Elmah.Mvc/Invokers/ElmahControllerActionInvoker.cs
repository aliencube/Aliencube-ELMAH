using System;
using System.Web.Mvc;

namespace Aliencube.Elmah.Mvc.Invokers
{
    /// <summary>
    /// This represents an entity responsible for invoking the action methods of a controller.
    /// </summary>
    /// <remarks>
    /// http://dotnetdarren.wordpress.com/2010/07/27/logging-on-mvc-part-1/
    /// </remarks>
    public class ElmahControllerActionInvoker : ControllerActionInvoker
    {
        private readonly IExceptionFilter _filter;

        /// <summary>
        /// Initialises a new instance of the ElmahControllerActionInvoker class.
        /// </summary>
        /// <param name="filter">Exception filter instance.</param>
        public ElmahControllerActionInvoker(IExceptionFilter filter)
        {
            if (filter == null)
                throw new ArgumentNullException("filter");

            this._filter = filter;
        }

        /// <summary>
        /// Retrieves information about the action filters.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>Returns the value of the action-method parameter.</returns>
        protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var filterInfo = base.GetFilters(controllerContext, actionDescriptor);

            filterInfo.ExceptionFilters.Add(this._filter);

            return filterInfo;
        }
    }
}