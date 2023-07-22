using Devpack.Selenium.Factories;
using Devpack.Selenium.Handlers.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Runtime.CompilerServices;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

[assembly: InternalsVisibleTo("Devpack.Selenium.Tests")]
namespace Devpack.Selenium.Handlers
{
    public class SeleniumHandler : IFirefoxSeleniumHandler, IChromeSeleniumHandler
    {
        public IWebDriver WebDriver { get; init; }
        public WebDriverWait Wait { get; init; }

        internal SeleniumHandler(Browser browser, string driverPath, bool headless)
        {
            WebDriver = WebDriverFactory.CreateWebDriver(browser, driverPath, headless);
            WebDriver.Manage().Window.Maximize();

            Wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(30));
        }

        public string GetAtualUrl()
        {
            return WebDriver.Url;
        }

        public void Navigate(string url)
        {
            WebDriver.Navigate().GoToUrl(url);
        }

        public void GoBackNavigate(int times = 1)
        {
            for (var i = 0; i < times; i++)
            {
                WebDriver.Navigate().Back();
            }
        }

        public bool ExistsByAnchor(string anchor)
        {
            return HasElement(By.CssSelector(anchor));
        }

        public void ClickLinkByText(string text)
        {
            var link = Wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText(text)));
            link.Click();
        }

        public void ClickByAnchor(string anchor)
        {
            var element = Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(anchor)));
            element.Click();
        }

        public IWebElement GetByAnchor(string anchor)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(anchor)));
        }

        public void SetTextBoxById(string id, string text)
        {
            var input = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
            input.SendKeys(text);
        }

        public void SetDropDownById(string id, string optionValue)
        {
            var dropdown = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
            var selectElement = new SelectElement(dropdown);

            selectElement.SelectByValue(optionValue);
        }

        public string GetTextByAnchor(string anchor)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(anchor))).Text;
        }

        public string GetInputValueById(string id)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)))
                .GetAttribute("value");
        }

        public IEnumerable<IWebElement> GetElementsByAnchor(string anchor)
        {
            return Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(anchor)));
        }

        private bool HasElement(By by)
        {
            try
            {
                WebDriver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void Dispose()
        {
            WebDriver.Quit();
            WebDriver.Dispose();
        }
    }
}