using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace BuggyCars.Pages
{
    public class UserProfilePage : BasePage
    {
        private readonly IWebDriver _driver;

        public UserProfilePage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        private IWebElement LogInUsernameText => _driver.FindElement(By.Id("username"));
        private IWebElement FirstnameText => _driver.FindElement(By.Id("firstName"));
        private IWebElement LastnameText => _driver.FindElement(By.Id("lastName"));
        private IWebElement GenderText => _driver.FindElement(By.Id("gender"));
        private IWebElement AgeText => _driver.FindElement(By.Id("age"));
        private IWebElement AddressText => _driver.FindElement(By.Id("address"));
        private IWebElement PhoneText => _driver.FindElement(By.Id("phone"));
        private IWebElement HobbyMenu => _driver.FindElement(By.Id("hobby"));
        private IWebElement CurrentPasswordText => _driver.FindElement(By.Id("currentPassword"));
        private IWebElement NewPasswordText => _driver.FindElement(By.Id("newPassword"));
        private IWebElement ConfirmPasswordText => _driver.FindElement(By.Id("newPasswordConfirmation"));
        private IWebElement SaveButton => _driver.FindElement(By.XPath("//button[contains(@class,'btn-default') and text()='Save']"));
        private IWebElement CancelLink => _driver.FindElement(By.XPath("//a[@class='btn' and text()='Cancel']"));
        private IWebElement SuccessErrorMessage => _driver.FindElement(By.XPath("//div[contains(@class,'my-form')]/form/div[@class='row']/descendant::div[contains(@class,'alert-success')]"));
        private By SuccessErrorMessageLocator => By.XPath("//div[@class='result alert alert-success hidden-md-down']");
        private By BasicSectionInUserProfileLocator => By.XPath("//div[@class='card']/h3[text()='Basic']");

        public void EnterUserInputParameterInUserProfilePage(string userInputParameter)
        {
            WaitForElementToDisplay(BasicSectionInUserProfileLocator, 5);
            var userInputParameters = userInputParameter.Split(",");
            foreach (var userInput in userInputParameters)
            {
                var parameter = userInput.Split(":");
                var fieldName = parameter[0].TrimStart().TrimEnd();
                var userText = parameter[1].TrimStart().TrimEnd();
                EnterUserTextIntoFieldName(fieldName, userText);
            }
        }
        
        public void EnterUserTextIntoFieldName(string fieldName, string userText)
        {
            SelectElement selectElement;
            switch(fieldName.ToUpper())
            {
                case "FIRSTNAME":
                    FirstnameText.Clear();
                    FirstnameText.SendKeys(userText);
                    break;
                case "LASTNAME":
                    LastnameText.Clear();
                    LastnameText.SendKeys(userText);
                    break;
                case "GENDER":
                    GenderText.Clear();
                    GenderText.SendKeys(userText);
                    break;
                case "AGE":
                    AgeText.Clear();
                    AgeText.SendKeys(userText);
                    break;
                case "ADDRESS":
                    AddressText.Clear();
                    AddressText.SendKeys(userText);
                    break;
                case "PHONE":
                    PhoneText.Clear();
                    PhoneText.SendKeys(userText);
                    break;
                case "HOBBY":
                    selectElement = new SelectElement(HobbyMenu);
                    selectElement.SelectByText(userText);
                    break;
                case "CURRENT PASSWORD":
                    CurrentPasswordText.Clear();
                    CurrentPasswordText.SendKeys(userText);
                    break;
                case "NEW PASSWORD":
                    NewPasswordText.Clear();
                    NewPasswordText.SendKeys(userText);
                    break;
                case "CONFIRM PASSWORD":
                    ConfirmPasswordText.Clear();
                    ConfirmPasswordText.SendKeys(userText);
                    break;
                default:
                    throw new ArgumentException($"{fieldName} not supported");
            }
        }

        public void ClickSaveButton()
        {
            SaveButton.Click();
        }

        public void ClickCancelLink()
        {
            CancelLink.Click();
        }

        public void VerifyProfileIsUpdatedSuccessfully()
        {
            WaitForElementToDisplay(SuccessErrorMessageLocator, 3);
            var actualMessageText = SuccessErrorMessage.Text;
            var expectedMessageText = GetExpectedUiDisplayedTextMessage("ProfileUpdateSuccess");
            Assert.AreEqual(expectedMessageText, actualMessageText, $"Actual message: {actualMessageText} does not match expected message: {expectedMessageText}");
        }
    }
}
