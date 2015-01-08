namespace FrontEnd.Infrastructure.Windsor
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using Castle.MicroKernel;

    /// <summary>
    /// The default controller factory implementation used by the framework.
    /// </summary>
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        /// <exception cref="ArgumentEmptyException"></exception>
        public WindsorControllerFactory(IKernel kernel)
        {
            if (kernel == null)
                throw new ArgumentNullException("kernel");
            _kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            _kernel.ReleaseComponent(controller);
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                var message = string.Format("The controller for path '{0}' could not be found.",
                    requestContext.HttpContext.Request.Path);
                throw new HttpException(404, message);
            }

            //return (IController)_kernel.Resolve(controllerType);

            // Our custom action invoker implementation
            var controller = _kernel.Resolve(controllerType) as Controller;
            if (controller != null)
                controller.ActionInvoker = _kernel.Resolve<IActionInvoker>();
            return controller;

        }
    }
}