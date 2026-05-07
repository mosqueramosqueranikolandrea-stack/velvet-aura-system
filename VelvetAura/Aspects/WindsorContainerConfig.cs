using Castle.Windsor;
using Castle.MicroKernel.Registration;
using VelvetAura.Services;
using VelvetAura.Aspects;

namespace VelvetAura.Aspects
{
    public static class WindsorContainerConfig
    {
        public static IWindsorContainer Configure()
        {
            var container = new WindsorContainer();

            // Registrar interceptores
            container.Register(
                Component.For<LoggingInterceptor>().LifestyleTransient(),
                Component.For<ErrorInterceptor>().LifestyleTransient()
            );

            // Registrar servicio con interceptores
            container.Register(
                Component.For<IPagoService>()
                    .ImplementedBy<PagoService>()
                    .Interceptors<LoggingInterceptor, ErrorInterceptor>()
                    .LifestyleTransient()
            );

            return container;
        }
    }
}