namespace FrontEnd.Infrastructure.Windsor
{
    using Castle.MicroKernel;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    /// <summary>
    /// Puts the windsor container registers the container with itself.
    /// </summary>
    public class KernelInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IWindsorContainer>().Instance(container));
            container.Register(Component.For<IKernel>().Instance(container.Kernel));
        }
    }
}