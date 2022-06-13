using BuggyCars.Settings;
using NUnit.Framework;
using OpenQA.Selenium;

namespace BuggyCars.Pages
{
    public class HomePage : BasePage
    {
        private readonly IWebDriver _driver;
        private readonly UserCredentialsContext _userCredentials;

        public HomePage(IWebDriver driver, UserCredentialsContext userCredentials) : base(driver)
        {
            _driver = driver;
            _userCredentials = userCredentials;
        }

        private IWebElement LogInButton => _driver.FindElement(By.XPath("//button[contains(@class, 'btn-success') and text() = 'Login']"));
        private By LogInButtonLocator => By.XPath("//button[contains(@class, 'btn-success') and text() = 'Login']");
        private IWebElement RegisterButton => _driver.FindElement(By.XPath("//a[contains(@class, 'btn-success-outline') and text() = 'Register']"));
        private By RegisterButtonLocator => By.XPath("//a[contains(@class, 'btn-success-outline') and text() = 'Register']");
        private IWebElement LoginText => _driver.FindElement(By.Name("login"));
        private By LoginTextLocator => By.Name("login");
        private IWebElement PasswordText => _driver.FindElement(By.Name("password"));
        private By PasswordTextLocator => By.Name("password");
        private IWebElement ProfileLink => _driver.FindElement(By.XPath("//a[contains(@class,'nav-link') and text()='Profile']"));
        private By ProfileLinkLocator => (By.XPath("//a[contains(@class,'nav-link') and text()='Profile']"));
        private IWebElement LogoutLink => _driver.FindElement(By.XPath("//a[contains(@class,'nav-link') and text()='Logout']"));
        private By LogoutLinkLocator => By.XPath("//a[contains(@class,'nav-link') and text()='Logout']");
        private By ListOfRegisteredModelsImgLocator => By.XPath("//h2[text()='Overall Rating']/following::img[1]");
        private By InvalidLoginWarningLabelLocator => By.XPath("//span[@class='label label-warning']");
        private IWebElement InvalidLoginWarningLabel => _driver.FindElement(By.XPath("//span[@class='label label-warning']"));
        public void ClickLoginButton()
        {
            LogInButton.Click();
        }

        public void ClickRegisterButton()
        {
            RegisterButton.Click();
        }

        public void ClickProfileLink()
        {
            WaitForElementToDisplay(ProfileLinkLocator, 3);
            ProfileLink.Click();
        }

        public void EnterUsernameAndPassword()
        {
            WaitForElementToDisplay(LoginTextLocator, 3);
            LoginText.Clear();
            LoginText.SendKeys(_userCredentials.Username);

            PasswordText.Clear();
            PasswordText.SendKeys(_userCredentials.Password);
        }

        public void EnterUsernameAndPassword(string username, string password)
        {
            WaitForElementToDisplay(LoginTextLocator, 3);
            LoginText.Clear();
            LoginText.SendKeys(username);

            PasswordText.Clear();
            PasswordText.SendKeys(password);
        }

        public void VerifyUserIsLoggedInSuccessfully()
        {
            WaitForElementToDisplay(ProfileLinkLocator, 5);
            Assert.IsTrue(ProfileLink.Displayed, "Profile link is not displayed but expected");
            Assert.IsTrue(LogoutLink.Displayed, "Logout link is not displayed but expected");
            VerifyElementIsNotDisplayed(LoginTextLocator, 1);
            VerifyElementIsNotDisplayed(PasswordTextLocator, 1);
            VerifyElementIsNotDisplayed(LogInButtonLocator, 1);
            VerifyElementIsNotDisplayed(RegisterButtonLocator, 1);
        }

        public void VerifyUserIsNotLoggedInSuccessfully()
        {
            WaitForElementToDisplay(InvalidLoginWarningLabelLocator, 5);
            Assert.IsTrue(InvalidLoginWarningLabel.Displayed, "Error message is not displayed but expected");
            Assert.AreEqual(InvalidLoginWarningLabel.Text, GetExpectedUiDisplayedTextMessage("InvalidLoginCredentials"));
            VerifyElementIsNotDisplayed(ProfileLinkLocator, 1);
            VerifyElementIsNotDisplayed(LogoutLinkLocator, 1);
        }

        public void ViewListOfAllRegisteredModels()
        {
            WaitForElementToDisplay(ListOfRegisteredModelsImgLocator, 3);
            _driver.FindElement(ListOfRegisteredModelsImgLocator).Click();
        }
    }
}
