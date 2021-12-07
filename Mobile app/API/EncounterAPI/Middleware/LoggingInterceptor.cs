using Castle.DynamicProxy;
using Newtonsoft.Json;
using Serilog;
using System;

namespace EncounterAPI.Middleware
{
    public class LoggingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();

                Log.Logger.Information($"Method {invocation.Method.Name} " +
                    $"called with these parameters: {JsonConvert.SerializeObject(invocation.Arguments, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}" +
                    $"returned this response: {JsonConvert.SerializeObject(invocation.ReturnValue, Formatting.None, new JsonSerializerSettings(){ReferenceLoopHandling = ReferenceLoopHandling.Ignore})}");
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Error happened in method: {invocation.Method}. Error: {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }
    }
}
