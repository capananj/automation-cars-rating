using BuggyCars.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Linq;

namespace BuggyCars.Pages
{    public class BasePage
    {
        private readonly IWebDriver _driver;        

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
        }
        private By BuggyRatingLink => By.XPath("//a[@class='navbar-brand' and text()='Buggy Rating']");

        public void NavigateToUrl()
        {
            _driver.Navigate().GoToUrl("https://buggy.justtestit.org/");            
        }

        public IWebElement WaitForElementToDisplay (By locator, int waitTimeInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(waitTimeInSeconds));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public void VerifyElementIsNotDisplayed(By by, int waitTimeInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(waitTimeInSeconds));
            var ex = Assert.Throws<NoSuchElementException>(() => _driver.FindElement(by));
            Assert.IsTrue(ex.Message.Contains("no such element: Unable to locate element"));
        }
        
        public void WaitUntilElementIsNotDisplayed(By by, int waitTimeInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(waitTimeInSeconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(by));
        }

        public bool IsElementPresent(By by, int waitTimeInSeconds)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(waitTimeInSeconds);
            try
            {
                _driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void ClickBuggyRatingLinkToNavigateToHomePage()
        {
            _driver.FindElement(BuggyRatingLink).Click();
        }   
        
        public string GetExpectedUiDisplayedTextMessage(string userAction)
        {
            userAction = userAction.TrimStart().TrimEnd();
            var uiDisplayedText = JsonConvert.DeserializeObject<UiDisplayedTextMessages>(File.ReadAllText(@".\UiDisplayedTextMessages.json"));
            var expectedUiDisplayedText = uiDisplayedText.UiDisplayedMessages.FirstOrDefault(u => u.UserAction == userAction).TextMessage;

            return expectedUiDisplayedText;
        }
    }
}
