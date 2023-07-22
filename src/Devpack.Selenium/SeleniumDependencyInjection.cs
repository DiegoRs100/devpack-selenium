using Devpack.Selenium.Handlers;
using Devpack.Selenium.Handlers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Devpack.Selenium
{
    public static class SeleniumDependencyInjection
    {
        public static IServiceCollection AddSeleniumWebDriver(this IServiceCollection services, Func<WebDriverBuilder, WebDriverBuilder> execute)
        {
            var config = execute.Invoke(new WebDriverBuilder())!;

            if (config.Browsers.Any(b => b == Browser.Firefox))
            {
                services.AddScoped<IFirefoxSeleniumHandler, SeleniumHandler>(obj =>
                    new SeleniumHandler(Browser.Firefox, config.WebDriverPath, config.Headless));
            }

            if (config.Browsers.Any(b => b == Browser.Chrome))
            {
                services.AddScoped<IChromeSeleniumHandler, SeleniumHandler>(obj =>
                    new SeleniumHandler(Browser.Chrome, config.WebDriverPath, config.Headless));
            }

            return services;
        }
    }
}