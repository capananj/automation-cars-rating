using BoDi;
using BuggyCars.Settings;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace BuggyCars
{
    [Binding]
    public sealed class Hooks
    {
        private readonly IObjectContainer _container;
        private readonly BrowserSettingsContext _browserSettingContext;
        private IWebDriver _driver;

        public Hooks(IObjectContainer container, BrowserSettingsContext browserSettingContext)
        {
            _container = container;
            _browserSettingContext = browserSettingContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            switch(_browserSettingContext.Browser)
            {
                case "chrome":
                    var options = new ChromeOptions();
                    if (_browserSettingContext.IsHeadless)
                    {
                        options.AddArgument("--headless");
                        options.AddArgument("--window-size=1920,1080");
                    }
                    _driver = new ChromeDriver(options);
                    _driver.Manage().Window.Maximize();
                    break;
                default:
                    throw new ArgumentException($"{_browserSettingContext.Browser} not supported");
            }

            _container.RegisterInstanceAs(_driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver = null;
            }
        }
    }
}
