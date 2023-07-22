namespace Devpack.Selenium
{
    public class WebDriverBuilder
    {
        public IReadOnlyList<Browser> Browsers { get; private set; } = default!;
        public string WebDriverPath { get; private set; } = string.Empty;
        public bool Headless { get; private set; } = true;

        public WebDriverBuilder UseBrowsers(params Browser[] browsers)
        {
            Browsers = browsers.Distinct().ToList();
            return this;
        }

        public WebDriverBuilder UseWebDriverPath(string webDriverPath)
        {
            WebDriverPath = webDriverPath;
            return this;
        }

        public WebDriverBuilder DisableHeadless()
        {
            Headless = false;
            return this;
        }
    }
}