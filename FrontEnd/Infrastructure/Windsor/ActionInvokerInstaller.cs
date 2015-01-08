namespace FrontEnd.Infrastructure.Windsor
{
    using System.Web.Mvc;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    /// <summary>
    /// Custom action invoker that will do property injection.
    /// </summary>
    public class ActionInvokerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component
                .For<IActionInvoker>()
                .ImplementedBy<WindsorActionInvoker>());
        }
    }
}