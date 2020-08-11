using NekoChxn;
using System;
using System.Threading.Tasks;

namespace NekoChxn
{
    public class Program
    {
        private readonly NekoChxnClient _Client = new NekoChxnClient();

        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _Client.Debug += LogInfo;
            NekoChxnResponse cuddle = await _Client.Cuddle();
            if (cuddle.Ok)
            {
                Console.WriteLine($"Result okay, the resulting Url is {cuddle.Url}");
            }
            else
            {
                Console.WriteLine($"Something went wrong. The request returned a statuscode of {cuddle.StatusCode}.\nError: {cuddle.ErrorMessage}");
            }
        }

        public Task LogInfo(string message)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }
    }
}