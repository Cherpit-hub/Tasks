using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace CoreLayer
{
    static public class Configuration
    {
        public static string BrowserType { get; private set; }
        public static string AppUrl { get; private set; }
        public static string EnvBrowser => Environment.GetEnvironmentVariable("Browser")!;

        static Configuration() {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            BrowserType = configuration["Browser"] ?? "Chrome";
            AppUrl = configuration["ApplicationUrl"] ?? string.Empty;
        }
    }
}
