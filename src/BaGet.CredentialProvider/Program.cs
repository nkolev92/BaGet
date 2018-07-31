using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BaGet.CredentialProvider
{
    public static class Program
    {

        public static readonly TraceSource Logger = new TraceSource("CredentialPlugin");

        public static async Task<int> Main(string[] args)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                tokenSource.Cancel();
                eventArgs.Cancel = true;
            };

            return -1;
        }
    }
}
