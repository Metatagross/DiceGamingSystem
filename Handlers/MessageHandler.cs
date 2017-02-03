using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DiceGamingSystem.Handlers
{
    public class MessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync ( HttpRequestMessage request ,
            CancellationToken cancellationToken )
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = base.SendAsync(request , cancellationToken);
            stopwatch.Stop();

            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            return result;
        }
    }
}