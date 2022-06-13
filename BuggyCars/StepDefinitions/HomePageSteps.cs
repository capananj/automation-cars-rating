using BuggyCars.Pages;
using TechTalk.SpecFlow;

namespace BuggyCars.StepDefinitions
{
    [Binding]
    public class HomePageSteps
    {
        private readonly HomePage _homePage;
        private readonly UserRegistrationPage _userRegistrationPage;

        public HomePageSteps(HomePage homePage, UserRegistrationPage userRegistrationPage)
        {
            _homePage = homePage;
            _userRegistrationPage = userRegistrationPage;
        }

        [Given(@"I navigate to the Buggy cars page")]
        public void GivenINavigateToTheBuggyCarsPage()
        {
            _homePage.NavigateToUrl();
        }

        [Given(@"I click Register button")]
        [When(@"I click Register button")]
        public void WhenIClickRegisterButton()
        {
            _homePage.ClickRegisterButton();
            _userRegistrationPage.WaitForUserRegistrationPageToLoad();
        }

        [Given(@"I click Login button")]
        [When(@"I click Login button")]
        public void WhenIClickLoginButton()
        {
            _homePage.ClickLoginButton();
            _homePage.ClickBuggyRatingLinkToNavigateToHomePage();
        }

        [Given(@"I enter username and password")]
        [When(@"I enter username and password")]
        public void WhenIEnterUsernameAndPassword()
        {
            _homePage.EnterUsernameAndPassword();
        }

        [Then(@"I verify user is able to login successfully")]
        public void ThenIVerifyUserIsAbleToLoginSuccessfully()
        {
            _homePage.VerifyUserIsLoggedInSuccessfully();
        }

        [When(@"I view the list of all registered model")]
        public void WhenIViewTheListOfAllRegisteredModel()
        {
            _homePage.ViewListOfAllRegisteredModels();
        }

        [Given(@"I click Profile link")]
        [When(@"I click Profile link")]
        public void GivenIClickProfileLink()
        {
            _homePage.ClickProfileLink();
        }

        [When(@"I enter username (.*) and password (.*)")]
        public void WhenIEnterUsernameDummyAndPasswordDummy(string username, string password)
        {
            _homePage.EnterUsernameAndPassword(username, password);
        }

        [Then(@"I verify user is not able to login successfully")]
        public void ThenIVerifyUserIsNotAbleToLoginSuccessfully()
        {
            _homePage.VerifyUserIsNotLoggedInSuccessfully();
        }
    }
}
