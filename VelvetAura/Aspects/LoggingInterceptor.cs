using System;
using Castle.DynamicProxy;

// ============================================================
//   PARADIGMA: ASPECTOS (AOP)
//    - Castle DynamicProxy
//    - Interceptor para logging automático
//    - Registra entrada y salida de métodos (concern transversal)
// ============================================================

namespace VelvetAura.Aspects
{
    public class LoggingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"[LOG] 🟢 Entrando a: {invocation.Method.Name}");
            invocation.Proceed();
            Console.WriteLine($"[LOG] 🔴 Saliendo de: {invocation.Method.Name}");
        }
    }
}