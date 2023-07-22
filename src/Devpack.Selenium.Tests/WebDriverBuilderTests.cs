using Xunit;
using FluentAssertions;

namespace Devpack.Selenium.Tests
{
    public class WebDriverBuilderTests
    {
        [Fact(DisplayName = "Deve popular o objeto 'Browsers' quando o método for chamado.")]
        public void UseBrowsers()
        {
            var builder = new WebDriverBuilder();
            builder.UseBrowsers(Browser.Chrome, Browser.Firefox, Browser.Chrome);

            builder.Browsers.Should().HaveCount(2)
                .And.Contain(Browser.Firefox)
                .And.Contain(Browser.Chrome);
        }

        [Fact(DisplayName = "Deve popular o objeto 'WebDriverPath' quando o método for chamado.")]
        public void WebDriverPath()
        {
            var expectedPath = Guid.NewGuid().ToString();

            var builder = new WebDriverBuilder();
            builder.UseWebDriverPath(expectedPath);

            builder.WebDriverPath.Should().Be(expectedPath);
        }

        [Fact(DisplayName = "Deve definir a propriedade 'Headless' como false quando o método for chamado.")]
        public void DisableHeadless()
        {
            var builder = new WebDriverBuilder();
            builder.DisableHeadless();

            builder.Headless.Should().BeFalse();
        }
    }
}