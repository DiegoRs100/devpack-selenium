using Devpack.Selenium.Handlers;
using FluentAssertions;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System.Reflection;
using Xunit;
using Devpack.Extensions.Types;

namespace Devpack.Selenium.Tests
{
    public class SeleniumHandlerTests
    {
        private readonly string _addressPage1;
        private readonly string _addressPage2;

        public SeleniumHandlerTests()
        {
            var currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!;

            _addressPage1 = $"{currentPath}/Common/Page1.html";
            _addressPage2 = $"{currentPath}/Common/Page2.html";
        }

        [Fact(DisplayName = "Deve instanciar corretamente o objeto quando o construtor for chamado.")]
        public void Constructor()
        {
            using var handler = new SeleniumHandler(Browser.Firefox, string.Empty, true);
            handler.WebDriver.Quit();

            var capabilities = handler.WebDriver.GetPropertyValue("Capabilities") as ICapabilities;
            var headless = capabilities!.GetCapability("moz:headless");

            handler.WebDriver.Should().BeOfType<FirefoxDriver>();
            handler.Wait.Timeout.Should().Be(TimeSpan.FromSeconds(30));
            headless.Should().Be(true);
        }

        // Esse teste valida ao mesmo tempo os métodos "GetAtualUrl" e "Navigate".
        [Fact(DisplayName = "Deve retornar a URL atual quando o método for chamado.")]
        public void GetAtualUrl()
        {
            var expectedUrl = $"file:///{_addressPage1.Replace(@"\", "/")}";

            using var handler = new SeleniumHandler(Browser.Chrome, string.Empty, true);
            handler.Navigate(_addressPage1);

            handler.GetAtualUrl().Should().Be(expectedUrl);
        }

        [Fact(DisplayName = "Deve navegar até a página anterior quando o método for chamado.")]
        public void GoBackNavigate()
        {
            using var handler = new SeleniumHandler(Browser.Chrome, string.Empty, true);

            handler.Navigate(_addressPage1);
            handler.Navigate(_addressPage2);
            handler.GoBackNavigate();

            handler.GetAtualUrl().Should().EndWith("Page1.html");
        }

        [Fact(DisplayName = "Deve retornar verdadeiro quando um elemento for identificado na página.")]
        public void ExistsByAnchor_WhenTrue()
        {
            using var handler = new SeleniumHandler(Browser.Chrome, string.Empty, true);
            handler.Navigate(_addressPage1);

            handler.ExistsByAnchor("#special-div").Should().BeTrue();
        }

        [Fact(DisplayName = "Deve retornar falso quando um elemento não for identificado na página.")]
        public void ExistsByAnchor_WhenFalse()
        {
            using var handler = new SeleniumHandler(Browser.Chrome, string.Empty, true);
            handler.Navigate(_addressPage2);

            handler.ExistsByAnchor("#special-div").Should().BeFalse();
        }

        [Fact(DisplayName = "Deve clicar no link quando existir um link com o texto informado.")]
        public void ClickLinkByText()
        {
            using var handler = new SeleniumHandler(Browser.Chrome, string.Empty, true);
            handler.Navigate(_addressPage1);

            handler.ClickLinkByText("Page2");

            handler.GetAtualUrl().Should().EndWith("Page2.html");
        }

        [Fact(DisplayName = "Deve clicar em um elemento quando ele existir com a âncora informada.")]
        public void ClickByAnchor()
        {
            using var handler = new SeleniumHandler(Browser.Chrome, string.Empty, true);
            handler.Navigate(_addressPage1);

            handler.ClickByAnchor("#link1");

            handler.GetAtualUrl().Should().EndWith("Page2.html");
        }

        [Fact(DisplayName = "Deve obter um elemento quando ele existir com a âncora informada.")]
        public void GetByAnchor()
        {
            using var handler = new SeleniumHandler(Browser.Chrome, string.Empty, true);
            handler.Navigate(_addressPage1);

            var element = handler.GetByAnchor("#link1");

            element.Text.Should().Be("Page2");
        }

        // Esse teste valida ao mesmo tempo os métodos "SetTextBoxById" e "GetInputValueById".
        [Fact(DisplayName = "Deve inserir um texto em um textbox quando ele existir com o id informado.")]
        public void SetTextBoxById()
        {
            var expectedText = Guid.NewGuid().ToString();

            using var handler = new SeleniumHandler(Browser.Chrome, string.Empty, true);
            handler.Navigate(_addressPage1);

            handler.SetTextBoxById("text-box", expectedText);
            var text = handler.GetInputValueById("text-box");

            text.Should().Be(expectedText);
        }

        [Fact(DisplayName = "Deve redirecionar a navegação para um IFrame quando ele estiver presente na página.")]
        public void SwithToFrameByAnchor()
        {
            using var handler = new SeleniumHandler(Browser.Chrome, string.Empty, true);
            handler.Navigate(_addressPage1);

            handler.SwithToFrameByAnchor("#frame-1");
            var text = handler.GetTextByAnchor("h");

            text.Should().Be("Page 2");
        }

        [Fact(DisplayName = "Deve retonar a navegação para a página principal quando a navegação estiver em um IFrame.")]
        public void ReturnFromFrame()
        {
            using var handler = new SeleniumHandler(Browser.Chrome, string.Empty, true);
            handler.Navigate(_addressPage1);

            handler.SwithToFrameByAnchor("#frame-1");
            handler.ReturnFromFrame();

            var text = handler.GetTextByAnchor("h");

            text.Should().Be("Page 1");
        }

        [Fact(DisplayName = "Deve selecionar a opção de um dropdown quando ele existir com o id informado.")]
        public void SetDropDownById()
        {
            using var handler = new SeleniumHandler(Browser.Chrome, string.Empty, true);
            handler.Navigate(_addressPage1);

            handler.SetDropDownById("select-box", "op-2");
            var element = handler.GetByAnchor("#select-box");

            element.GetAttribute("value").Should().Be("op-2");
        }

        [Fact(DisplayName = "Deve retornar o texto de um elemento quando ele existir com a âncora informada.")]
        public void GetTextByAnchor()
        {
            using var handler = new SeleniumHandler(Browser.Chrome, string.Empty, true);
            handler.Navigate(_addressPage1);

            var text = handler.GetTextByAnchor("#title");

            text.Should().Be("Page 1");
        }

        [Fact(DisplayName = "Deve retornar uma lista de elementos quando eles existirem com uma âncora informado.")]
        public void GetElementsByAnchor()
        {
            using var handler = new SeleniumHandler(Browser.Chrome, string.Empty, true);
            handler.Navigate(_addressPage1);

            var elements = handler.GetElementsByAnchor("#select-box > option");

            elements.Should().HaveCount(3)
                .And.Contain(e => e.Text == "Option1")
                .And.Contain(e => e.Text == "Option2")
                .And.Contain(e => e.Text == "Option3");
        }
    }
}