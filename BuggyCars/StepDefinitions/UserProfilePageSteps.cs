using BuggyCars.Pages;
using TechTalk.SpecFlow;

namespace BuggyCars.StepDefinitions
{
    [Binding]
    public class UserProfilePageSteps
    {
        private readonly UserProfilePage _userProfilePage;

        public UserProfilePageSteps(UserProfilePage userProfilePage)
        {
            _userProfilePage = userProfilePage;
        }

        [When(@"I enter user details (.*)")]
        public void WhenIEnterUserDetails(string userInputParameters)
        {
            _userProfilePage.EnterUserInputParameterInUserProfilePage(userInputParameters);
        }

        [When(@"I click Save button")]
        public void WhenIClickSaveButton()
        {
            _userProfilePage.ClickSaveButton();
        }

        [Then(@"I verify that profile has been updated successfully")]
        public void ThenIVerifyThatProfileHasBeenUpdatedSuccessfully()
        {
            _userProfilePage.VerifyProfileIsUpdatedSuccessfully();
        }
    }
}
