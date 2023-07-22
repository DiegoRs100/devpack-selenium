# What does it do?

This library aims to create abstractions for using selenium with various web drivers.

# How to use it?

To instantiate the application, just inject it by dependency.

```csharp
    public void Configure()
    {
        services.AddSeleniumWebDriver(options => options
            .UseBrowsers(Browser.Firefox, Browser.Chrome)
            .UseWebDriverPath("c://webdrivers")
            .DisableHeadless()
        );
    }

    // Or

    public void Configure()
    {
        services.AddSeleniumWebDriver(options => options
            .UseBrowsers(Browser.Firefox)
            .UseWebDriverPath("c://webdrivers")
        );

        services.AddSeleniumWebDriver(options => options
            .UseBrowsers(Browser.Chrome)
            .UseWebDriverPath("c://webdrivers")
            .DisableHeadless()
        );
    }
```

where:
- 'UseBrowsers' - Defines which browsers we will work with. (Currently only supported by Firefox and Google Chrome)
- 'UseWebDriverPath' - Define if we are going to use any specific webdriver. (If not informed, the library will use the version installed on the machine)
    - [Google Chrome](https://chromedriver.chromium.org/downloads)
    - [GFirefox](https://github.com/mozilla/geckodriver/releases)
- 'DisableHeadless' - When called, it indicates that the automation operation should be displayed on the screen.

# Handlers

With the configuration performed at application startup, just inject the browser interface you want to use by dependency.


```csharp
    public class Automation
    {
        private readonly IFirefoxSeleniumHandler _firefoxHandler;
        private readonly IChromeSeleniumHandler _chromeHandler;

        public Automation(IFirefoxSeleniumHandler firefoxHandler,
                          IChromeSeleniumHandler chromeHandler)
        {
            _firefoxHandler = firefoxHandler;
            _chromeHandler = chromeHandler;
        }

        // Example: 
        // Performs a google search on selenium documentation and enters the official documentation link.
        public void Automate()
        {
            _firefoxHandler.Navigate("www.google.com.br");

            _firefoxHandler.SetTextBoxById("search-box", "selenium documentation");
            _firefoxHandler.ClickByAnchor("#search-button");

            _firefoxHandler.ClickLinkByText("The Selenium Browser Automation Project");

            _firefoxHandler.Dispose();
        }
    }
```

With that, just use the methods and abstractions available in the handlers. All of them are self-explanatory, so they are not included in this documentation.