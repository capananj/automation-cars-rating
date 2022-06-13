using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using BuggyCars.Settings;

namespace BuggyCars.Pages
{
    public class UserRegistrationPage : BasePage
    {
        private readonly IWebDriver _driver;
        private readonly UserCredentialsContext _userCredentialsContext;

        public UserRegistrationPage(IWebDriver driver, UserCredentialsContext userCredentialsContext) : base(driver)
        {
            _driver = driver;
            _userCredentialsContext = userCredentialsContext;
        }

        private By RegisterPageHeaderLocator => By.XPath("//div[@class='container my-form']/descendant::h2[contains(text(), 'Register with Buggy Cars Rating')]");
        private IWebElement LoginText => _driver.FindElement(By.Id("username"));
        private IWebElement FirstNameText => _driver.FindElement(By.Id("firstName"));
        private IWebElement LastNameText => _driver.FindElement(By.Id("lastName"));
        private IWebElement PasswordText => _driver.FindElement(By.Id("password"));
        private IWebElement ConfirmPasswordText => _driver.FindElement(By.Id("confirmPassword"));
        private IWebElement RegisterSubmitButton => _driver.FindElement(By.XPath("//button[contains(@class,'btn-default') and contains(text(),'Register')]"));
        private IWebElement CancelButton => _driver.FindElement(By.XPath("//a[contains(@class,'btn') and contains(text(),'Cancel')]"));
        private By SuccessMessageLocator => By.XPath("//div[contains(@class,'alert-success')]");
        private By ErrorMessageLocator => By.XPath("//div[contains(@class,'alert alert-danger')]");
        public void GenerateAndEnterUniqueLogInDetail()
        {
            var uniqueLoginId = Guid.NewGuid().ToString().Substring(0,10);
            _userCredentialsContext.Username = uniqueLoginId;
            Console.WriteLine("Login: " + uniqueLoginId);
            EnterUserTextIntoFieldName("Login", uniqueLoginId);
        }

        public void EnterUserRegistrationDetails(Table table)
        {
            var userDetailsTable = table.CreateSet<(string fieldName, string textInput)>();
            foreach (var (fieldName, userText) in userDetailsTable)
            {
                EnterUserTextIntoFieldName(fieldName, userText);
            }
        }

        public void EnterUserTextIntoFieldName(string fieldName, string userText)
        {
            userText = userText.TrimStart().TrimEnd();
            switch(fieldName.Trim().ToUpper())
            {
                case "LOGIN":
                    LoginText.Clear();
                    LoginText.SendKeys(userText);
                    break;
                case "FIRSTNAME":
                    FirstNameText.Clear();
                    FirstNameText.SendKeys(userText);
                    break;
                case "LASTNAME":
                    LastNameText.Clear();
                    LastNameText.SendKeys(userText);
                    break;
                case "PASSWORD":
                    PasswordText.Clear();
                    PasswordText.SendKeys(userText);
                    _userCredentialsContext.Password = userText;
                    break;
                case "CONFIRM PASSWORD":
                    ConfirmPasswordText.Clear();
                    ConfirmPasswordText.SendKeys(userText);
                    break;
                default:
                    throw new ArgumentException($"{fieldName} not supported");
            }
        }

        public void WaitForUserRegistrationPageToLoad()
        {
            WaitForElementToDisplay(RegisterPageHeaderLocator, 3);
        }

        public void ClickRegisterSubmitButton()
        {
            RegisterSubmitButton.Click();
        }

        public void ClickCancelButton()
        {
            CancelButton.Click();
        }

        public void VerifySuccessMessageIsDisplayed()
        {
            WaitForElementToDisplay(SuccessMessageLocator, 5);
            var actualMessageText = _driver.FindElement(SuccessMessageLocator).Text;
            var expectedMessageText = GetExpectedUiDisplayedTextMessage("RegistrationSuccessful");
            Assert.AreEqual(expectedMessageText, actualMessageText, $"Actual message: {actualMessageText} does not match expected message: {expectedMessageText}");
        }
    }
}
