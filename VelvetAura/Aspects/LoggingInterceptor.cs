using System;
using Castle.DynamicProxy;

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