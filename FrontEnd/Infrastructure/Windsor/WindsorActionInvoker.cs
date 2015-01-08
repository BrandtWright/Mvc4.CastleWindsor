namespace FrontEnd.Infrastructure.Windsor
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Castle.Windsor;

    /// <summary>
    /// 
    /// </summary>
    public class WindsorActionInvoker : ControllerActionInvoker
    {
        private readonly IWindsorContainer _container;

        /// <summary>
        /// A ControllerActionInvoker that is capable of injecting dependencied into
        /// Action/Exception/Result/Authorization filters.
        /// </summary>
        /// <param name="container"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public WindsorActionInvoker(IWindsorContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            _container = container;
        }

        protected override ActionExecutedContext InvokeActionMethodWithFilters(
            ControllerContext controllerContext, 
            IList<IActionFilter> filters, 
            ActionDescriptor actionDescriptor, 
            IDictionary<string, object> parameters)
        {
            foreach (var actionFilter in filters)
            {
                _container.Kernel.InjectProperties(actionFilter);
            }
            return base.InvokeActionMethodWithFilters(controllerContext, filters, actionDescriptor, parameters);
        }

        protected override ResultExecutedContext InvokeActionResultWithFilters(
            ControllerContext controllerContext, 
            IList<IResultFilter> filters, 
            ActionResult actionResult)
        {
            foreach (var resultFilter in filters)
            {
                _container.Kernel.InjectProperties(resultFilter);
            }
            return base.InvokeActionResultWithFilters(controllerContext, filters, actionResult);
        }

        protected override AuthorizationContext InvokeAuthorizationFilters(
            ControllerContext controllerContext, 
            IList<IAuthorizationFilter> filters,
            ActionDescriptor actionDescriptor)
        {
            foreach (var authorizationFilter in filters)
            {
                _container.Kernel.InjectProperties(authorizationFilter);
            }
            return base.InvokeAuthorizationFilters(controllerContext, filters, actionDescriptor);
        }

        protected override ExceptionContext InvokeExceptionFilters(
            ControllerContext controllerContext, 
            IList<IExceptionFilter> filters, 
            Exception exception)
        {
            foreach (var exceptionFilter in filters)
            {
                _container.Kernel.InjectProperties(exceptionFilter);
            }
            return base.InvokeExceptionFilters(controllerContext, filters, exception);
        }
    }
}