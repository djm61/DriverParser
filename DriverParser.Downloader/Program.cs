using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DriverParser.Downloader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //https://docs.google.com/spreadsheets/d/1kEMqILMljdKPl8XdpuuguxXoSS_04tkd3wA2xeakDco/edit#gid=2083413504
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();

            ConfigureServices(serviceCollection);

            //var builder = new HostBuilder()
            //    .ConfigureServices((hostContext, services) =>
            //    {
            //        services.AddHttpClient;
            //        //services.AddTransient<IMyService, MyService>();
            //    }).UseConsoleLifetime();


            var serviceProvider = serviceCollection.BuildServiceProvider();
            serviceProvider
                .GetService<ILoggerFactory>()
                //.AddConsole()
                //.AddDebug()
                //.AddLog4Net()
                ;

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async static Task MainAsync(string[] args)
        {
            var url = "https://raceapp.eu/#";
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent",
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.87 Safari/537.36");
                //var response = await client.GetAsync(@"http://raceapp.eu/esr#/Series/430?tab=booking");
                var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                var pageContents = await response.Content.ReadAsStringAsync();

                //var request = WebRequest.Create(url) as HttpWebRequest;
                //request.AllowReadStreamBuffering = false;
                //request.AllowWriteStreamBuffering = false;

                //byte[] buffer;
                //using (var response = await request.GetResponseAsync() as HttpWebResponse)
                //{
                //    using (var stream = response.GetResponseStream())
                //    {
                //        buffer = new byte[1000];
                //        var byteCount = await stream.ReadAsync(buffer, 0, buffer.Length);
                //        request.Abort();
                //    }
                //}

                //var pageContents = Encoding.UTF8.GetString(buffer);

                Console.WriteLine(pageContents);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            Console.ReadLine();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            var builder = new ConfigurationBuilder()
                //.SetBasePath(Directory.GetCurrentDirectory())
                //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                ;

            var configuration = builder.Build();

            
        }
    }
}
