using Devpack.Selenium.Factories;
using Xunit;
using Devpack.Extensions.Types;
using FluentAssertions;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.ComponentModel;

namespace Devpack.Selenium.Tests
{
    public class WebDriverFactoryTests
    {
        [Theory(DisplayName = "Deve retornar um webbrowser do firefox quando o enum 'Firefox' for informado.")]
        [InlineData(true)]
        [InlineData(false)]
        public void CreateWebDriver_WhenFirefox(bool expectedHeadless)
        {
            // Arrange / Act
            using var webDriver = WebDriverFactory.CreateWebDriver(Browser.Firefox, string.Empty, expectedHeadless);
            webDriver.Quit();

            var capabilities = webDriver.GetPropertyValue("Capabilities") as ICapabilities;
            var headless = capabilities!.GetCapability("moz:headless");

            // Asserts
            webDriver.Should().BeOfType<FirefoxDriver>();
            headless.Should().Be(expectedHeadless);
        }

        [Theory(DisplayName = "Deve retornar um webbrowser do chrome quando o enum 'Chrome' for informado.")]
        [InlineData(true)]
        [InlineData(false)]
        public void CreateWebDriver_WhenChrome(bool expectedHeadless)
        {
            // Arrange / Act
            using var webDriver = WebDriverFactory.CreateWebDriver(Browser.Chrome, string.Empty, expectedHeadless);
            webDriver.Quit();

            // Asserts
            webDriver.Should().BeOfType<ChromeDriver>();
        }

        [Fact(DisplayName = "Deve lançar uma exception quando um browser inválido for informado.")]
        public void CreateWebDriver_WhenInvalidBrowser()
        {
            var act = () => WebDriverFactory.CreateWebDriver((Browser)99, string.Empty, true);
            act.Should().Throw<InvalidEnumArgumentException>();
        }
    }
}