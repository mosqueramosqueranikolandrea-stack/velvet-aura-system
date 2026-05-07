using System;
using Castle.DynamicProxy;

namespace VelvetAura.Aspects
{
    public class ErrorInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] ❌ {ex.Message}");
            }
        }
    }
}