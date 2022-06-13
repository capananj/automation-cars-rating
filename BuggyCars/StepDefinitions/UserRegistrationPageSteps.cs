using BuggyCars.Pages;
using TechTalk.SpecFlow;

namespace BuggyCars.StepDefinitions
{
    [Binding]
    public class UserRegistrationPageSteps
    {
        private readonly UserRegistrationPage _userRegistrationPage;

        public UserRegistrationPageSteps(UserRegistrationPage userRegistrationPage)
        {
            _userRegistrationPage = userRegistrationPage;
        }

        [Given(@"I enter a unique login id")]
        [When(@"I enter a unique login id")]
        public void WhenIEnterAUniqueLoginId()
        {
            _userRegistrationPage.GenerateAndEnterUniqueLogInDetail();
        }

        [Given(@"I enter user registration details")]
        [When(@"I enter user registration details")]
        public void WhenIEnterUserRegistrationDetails(Table table)
        {
            _userRegistrationPage.EnterUserRegistrationDetails(table);
        }

        [Given(@"I click Register button to submit user details")]
        [When(@"I click Register button to submit user details")]
        public void WhenIClickRegisterButtonToSubmitUserDetails()
        {
            _userRegistrationPage.ClickRegisterSubmitButton();
        }

        [Given(@"I verify that user is registered successfully")]
        [Then(@"I verify that user is registered successfully")]
        public void ThenIVerifyThatUserIsRegisteredSuccessfully()
        {
            _userRegistrationPage.VerifySuccessMessageIsDisplayed();
        }
    }
}
