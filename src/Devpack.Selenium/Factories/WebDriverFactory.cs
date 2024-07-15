using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.ComponentModel;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Devpack.Selenium.Tests")]
namespace Devpack.Selenium.Factories
{
    internal static class WebDriverFactory
    {
        internal static IWebDriver CreateWebDriver(Browser browser, string driverPath, bool headless)
        {
            return browser switch
            {
                Browser.Firefox => CreateFirefoxWebDriver(driverPath, headless),
                Browser.Chrome => CreateChromeWebDriver(driverPath, headless),
                _ => throw new InvalidEnumArgumentException()
            };
        }

        private static IWebDriver CreateFirefoxWebDriver(string driverPath, bool headless)
        {
            var optionsFireFox = new FirefoxOptions();

            if (headless)
                optionsFireFox.AddArgument("--headless");

            return new FirefoxDriver(driverPath, optionsFireFox);
        }

        private static IWebDriver CreateChromeWebDriver(string driverPath, bool headless)
        {
            var options = new ChromeOptions();

            if (headless)
                options.AddArgument("--headless");

            options.AddArgument("load-extension=/Users\\Diego\\AppData\\Local\\Google\\Chrome\\User Data\\Default\\Extensions\\hniebljpgcogalllopnjokppmgbhaden\\3.3.1_0");

            return new ChromeDriver(driverPath, options);
        }
    }
}