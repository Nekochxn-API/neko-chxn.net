# Neko-chxn

This is the official [neko-chxn.xyz](https://neko-chxn.xyz/) C# wrapper. It uses `System.Threading.Tasks` to do operations asynchronously and returns a Task that you will have to await. <br><br>This package has the dependency [Json.NET](https://www.newtonsoft.com/json) to parse the api response.

## Usage

All endpoints return the class NekoChxnResponse which has the following properties. Types ending with a `?` are potentially null.

| Property     | Type    | Description                                             | Example                                                          |
| ------------ | ------- | ------------------------------------------------------- | ---------------------------------------------------------------- |
| Ok           | bool    | A boolean indicating whether the request was successful | true                                                             |
| Endpoint     | string  | The endpoint that was fetched                           | cuddle/img                                                       |
| StatusCode   | int     | The status code the request ended with                  | 200                                                              |
| Url          | string? | The url of the resulting gif                            | https://api.neko-chxn.xyz/v1/cuddle/output/cuddle_002.gif        |
| ErrorMessage | string? | The error if any occurred                               | Response status code does not indicate success: 404 (Not Found). |

Minimal Example

```cs
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
    }
}

// Console output:
// Result okay, the resulting Url is https://api.neko-chxn.xyz/v1/cuddle/output/cuddle_002.gif
```

Example using the debug output:

```cs
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
            var cuddle = await _Client.Cuddle();
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

// Console output:
// [NekoChxn] Successfully fetched the cuddle/img endpoint
// Result okay, the resulting Url is https://api.neko-chxn.xyz/v1/cuddle/output/cuddle_002.gif
```

### Current Endpoints

| Endpoint | Rating | Description      |
| -------- | ------ | ---------------- |
| Blush    | SFW    | Get a blush gif  |
| Cry      | SFW    | Get a crying gif |
| Cuddle   | SFW    | Get a cuddle gif |
| Dance    | SFW    | Get a dance gif  |
| Hug      | SFW    | Get a hug gif    |
| Kick     | SFW    | Get a kick gif   |
| Kiss     | SFW    | Get a kiss gif   |
| Love     | SFW    | Get a love gif   |
| Pat      | SFW    | Get a pat gif    |
| Punch    | SFW    | Get a punch gif  |
| Smirk    | SFW    | Get a smirk gif  |
| Tickle   | SFW    | Get a tickle gif |
| Yell     | SFW    | Get a yell gif   |
