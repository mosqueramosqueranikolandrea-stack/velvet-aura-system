using Castle.Windsor;
using Castle.MicroKernel.Registration;
using VelvetAura.Services;
using VelvetAura.Aspects;

// ============================================================
// PARADIGMA: ASPECTOS (AOP) + Inyección de Dependencias
//    - Castle Windsor como contenedor DI
//    - Registro de servicios por INTERFAZ (requisito del profe)
//    - Configuración de interceptores para logging y errores
// ============================================================

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