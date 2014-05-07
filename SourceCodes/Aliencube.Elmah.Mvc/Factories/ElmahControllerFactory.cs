using Aliencube.Elmah.Mvc.Attributes;
using Aliencube.Elmah.Mvc.Invokers;
using System.Web.Mvc;
using System.Web.Routing;

namespace Aliencube.Elmah.Mvc.Factories
{
    /// <summary>
    /// This represents the controller factory entity inherited from <c>DefaultControllerFactory</c> and registered for ELMAH.
    /// </summary>
    /// <remarks>
    /// http://dotnetdarren.wordpress.com/2010/07/27/logging-on-mvc-part-1/
    /// </remarks>
    public class ElmahControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// Creates the specified controller by using the specified request context.
        /// </summary>
        /// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route data.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <returns>Returns the controller instance.</returns>
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            var controller = base.CreateController(requestContext, controllerName);

            var c = controller as Controller;
            if (c != null)
                c.ActionInvoker = new ElmahControllerActionInvoker(new ElmahHandleErrorAttribute());

            return controller;
        }
    }
}