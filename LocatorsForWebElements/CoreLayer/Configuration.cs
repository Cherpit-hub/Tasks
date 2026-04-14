using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsForWebElements.CoreLayer
{
    static public class Configuration
    {
        public static string BrowserType { get; private set; }
        public static string AppUrl { get; private set; }
        public static string ChromeOptions { get; private set; }
        public static string FirefoxOptions { get; private set; }
        public static string EdgeOptions { get; private set; }

        static Configuration() {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            BrowserType = configuration["Browser"] ?? "Chrome";
            AppUrl = configuration["ApplicationUrl"] ?? string.Empty;
            ChromeOptions = configuration["ChromeOptions"] ?? string.Empty;
            FirefoxOptions = configuration["FirefoxOptions"] ?? string.Empty;
            EdgeOptions = configuration["EdgeOptions"] ?? string.Empty;
        }
    }
}
