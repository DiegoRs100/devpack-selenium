﻿using Devpack.Selenium.Handlers.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Devpack.Selenium.Tests
{
    public class SeleniumDependencyInjectionTests
    {
        [Fact(DisplayName = "Deve configurar a injeção dos serviços do selenuim quando o método for chamado.")]
        public void AddSeleniumWebDriver()
        {
            var services = new ServiceCollection();

            services.AddSeleniumWebDriver(options => options
                .UseBrowsers(Browser.Firefox, Browser.Chrome)
                .UseWebDriverPath("c://webdrivers")
                .DisableHeadless()
            );

            var serviceProvider = services.BuildServiceProvider();

            using var firefoxHandler = serviceProvider.GetService<IFirefoxSeleniumHandler>();
            using var chromeHandler = serviceProvider.GetService<IChromeSeleniumHandler>();

            firefoxHandler.Should().NotBeNull();
            chromeHandler.Should().NotBeNull();
        }
    }
}