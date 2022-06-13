using BuggyCars.Pages;
using TechTalk.SpecFlow;

namespace BuggyCars.StepDefinitions
{
    [Binding]
    public class ListOfRegisteredModelsPageSteps
    {
        private ListOfRegisteredModelsPage _listOfRegisteredModelsPage;

        public ListOfRegisteredModelsPageSteps(ListOfRegisteredModelsPage listOfRegisteredModelsPage)
        {
            _listOfRegisteredModelsPage = listOfRegisteredModelsPage;
        }        

        [When(@"I click View more link on any make and model")]
        public void WhenIClickViewMoreLinkOnAnyMakeAndModel()
        {
            _listOfRegisteredModelsPage.ClickRandomViewMoreLink();
        }

        [When(@"I enter comment (.*)")]
        public void WhenIEnterComment(string userEnteredComment)
        {
            _listOfRegisteredModelsPage.EnterVoteComment(userEnteredComment);
        }

        [When(@"I click Vote button")]
        public void WhenIClickVoteButton()
        {
            _listOfRegisteredModelsPage.ClickVoteButton();
        }

        [Then(@"I verify that vote is submitted successfully")]
        public void ThenIVerifyThatVoteIsSubmittedSuccessfully()
        {
            _listOfRegisteredModelsPage.VerifyVoteIsSubmittedSuccessfully();
        }

        [Then(@"I verify vote count has incremented")]
        public void ThenIVerifyVoteCountHasIncremented()
        {
            _listOfRegisteredModelsPage.VerifyVoteCountIsIncrementedAfterSuccessfulVote();
        }

        [Then(@"I verify that unregistered user cannot submit a vote")]
        public void ThenIVerifyThatUnregisteredUserCannotSubmitAVote()
        {
            _listOfRegisteredModelsPage.VerifyUnregisteredUserCannotSubmitAVote();
        }

        [Then(@"I verify that comment (.*) displayed in the table")]
        public void ThenIVerifyThatCommentDisplayedInTheTable(string condition)
        {
            _listOfRegisteredModelsPage.VerifyCommentAddedInTheTable(condition);
        }

        [Then(@"I verify that car make and model table is displayed")]
        public void ThenIVerifyThatCarMakeAndModelTableIsDisplayed()
        {
            _listOfRegisteredModelsPage.VerifyTableOfCarMakeAndModelIsDisplayed();
        }

    }
}
