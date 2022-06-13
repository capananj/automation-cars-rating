using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace BuggyCars.Pages
{
    public class ListOfRegisteredModelsPage : BasePage
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _driver;
        public ListOfRegisteredModelsPage(ScenarioContext scenarioContext, IWebDriver driver) : base(driver)
        {
            _scenarioContext = scenarioContext;
            _driver = driver;
        }

        private IWebElement CommentTextArea => _driver.FindElement(By.Id("comment"));
        private By CommentTextAreaLocator => By.Id("comment");
        private IWebElement VoteButton => _driver.FindElement(By.XPath("//button[contains(@class,'btn-success') and text()='Vote!']"));
        private By VoteButtonLocator => By.XPath("//button[contains(@class,'btn-success') and text()='Vote!']");
        private By VotesSectionLocator => By.XPath("//div[@class='col-lg-4']/descendant::h4[contains(text(), 'Votes')]");
        private IWebElement CurrentVoteCount => _driver.FindElement(By.XPath("//div[@class='card-block']/h4[text()='Votes: ']"));
        private IWebElement VoteTextMessage => _driver.FindElement(By.XPath("//div[@class='card-block']/h4[text()='Votes: ']/following::div/p[@class='card-text']"));
        private By VoteTextMessageLocator => By.XPath("//div[@class='card-block']/h4[text()='Votes: ']/following::div/p[@class='card-text']");
        private IList<IWebElement> MakeModelList => _driver.FindElements(By.XPath("//div[@class='container']/descendant::tbody/tr"));
        private IList<IWebElement> CommentList => _driver.FindElements(By.XPath("//div[@class='container']/descendant::tbody/tr"));
        private By TableLocator => By.XPath("//div[@class='container']/descendant::tbody");
        
        public void ClickRandomViewMoreLink()
        {
            WaitForElementToDisplay(TableLocator, 5);
            var rowCount = MakeModelList.Count;
            Random rnd = new Random();      
            
            if (rowCount > 0)
            {
                var rowNum = rnd.Next(1, rowCount);
                var viewMoreLink = By.XPath($"//tr[{rowNum}]/td[7]/child::a");
                if (IsElementPresent(viewMoreLink, 1))
                {
                    _driver.FindElement(viewMoreLink).Click();
                    WaitForElementToDisplay(VotesSectionLocator, 5);
                    _scenarioContext["BeforeVoteCount"] = GetCurrentVoteCount();
                    _scenarioContext["BeforeVoteCommentListCount"] = GetCurrentCommentListCount();                    
                }
            }
            else
            {
                throw new Exception("List of registered models does not display any car make / model");
            }
        }

        public void EnterVoteComment(string userEnteredComment)
        {
            if (!string.IsNullOrEmpty(userEnteredComment))
            {
                CommentTextArea.Clear();
                CommentTextArea.SendKeys(userEnteredComment);
                _scenarioContext["Comment"] = userEnteredComment;
            }            
        }

        public void ClickVoteButton()
        {
            VoteButton.Click();
            _scenarioContext["DateVoteSubmitted"] = DateTime.Now.ToString("MMM dd, yyyy, h:mm");
            WaitUntilElementIsNotDisplayed(VoteButtonLocator, 3);
        }

        public void VerifyVoteIsSubmittedSuccessfully()
        {
            WaitForElementToDisplay(VoteTextMessageLocator, 3);
            var actualSuccessMessageDisplayed = VoteTextMessage.Text;
            var expectedSuccessMessage = GetExpectedUiDisplayedTextMessage("VoteSubmitted");

            Assert.AreEqual(expectedSuccessMessage, actualSuccessMessageDisplayed, $"Actual Message: {actualSuccessMessageDisplayed} does not match Expected Message {expectedSuccessMessage}");
            VerifyElementIsNotDisplayed(VoteButtonLocator, 1);
            VerifyElementIsNotDisplayed(CommentTextAreaLocator, 1);
        }

        public void VerifyVoteCountIsIncrementedAfterSuccessfulVote()
        {
            var expectedAfterVoteCount = ((int)_scenarioContext["BeforeVoteCount"]) + 1;
            var actualAfterVoteCount = GetCurrentVoteCount();
            Assert.AreEqual(expectedAfterVoteCount, actualAfterVoteCount, $"Actual Vote Count {actualAfterVoteCount} does not match Expected Vote Count {expectedAfterVoteCount}");
        }

        public void VerifyUnregisteredUserCannotSubmitAVote()
        {
            var actualVoteMessageDisplayed = VoteTextMessage.Text; ;
            var expectedVotesMessage = GetExpectedUiDisplayedTextMessage("VoteWithoutLogin");

            Assert.AreEqual(expectedVotesMessage, actualVoteMessageDisplayed, $"Actual Message: {actualVoteMessageDisplayed} does not match Expected Message {expectedVotesMessage}");
            VerifyElementIsNotDisplayed(VoteButtonLocator, 1);
            VerifyElementIsNotDisplayed(CommentTextAreaLocator, 1);
        }

        public void VerifyCommentAddedInTheTable(string condition)
        {
            WaitForElementToDisplay(TableLocator, 3);
            int expectedVoteCommentListCount;
            var actualVoteCommentListCount = GetCurrentCommentListCount();

            var expectedDate = (string)_scenarioContext["DateVoteSubmitted"];
            var actualDate = _driver.FindElement(By.XPath($"//div[@class='container']/descendant::tbody/tr[1]/td[1]")).Text;
            
            var expectedComment = _scenarioContext.ContainsKey("Comment") ? (string)_scenarioContext["Comment"] : string.Empty;
            var actualComment = _driver.FindElement(By.XPath($"//div[@class='container']/descendant::tbody/tr[1]/td[3]")).Text;

            if (condition.ToLower() == "is" && !string.IsNullOrEmpty(expectedComment))
            {
                expectedVoteCommentListCount = ((int)_scenarioContext["BeforeVoteCommentListCount"]) + 1;
                Assert.IsTrue(actualDate.Contains(expectedDate), $"Actual date {actualDate} does not contain Expected date {expectedDate}");
                Assert.AreEqual(expectedComment, actualComment, $"Actual comment {actualComment} does not match Expected comment {expectedComment}");
            }
            else
            {
                expectedVoteCommentListCount = (int)_scenarioContext["BeforeVoteCommentListCount"];                
            }
            Assert.AreEqual(expectedVoteCommentListCount, actualVoteCommentListCount, $"Actual comment list count {actualVoteCommentListCount} does not match expected comment list count");
        }

        public void VerifyTableOfCarMakeAndModelIsDisplayed()
        {
            WaitForElementToDisplay(TableLocator, 5);
            Assert.IsTrue(MakeModelList.Count > 0, "No car make or model is displayed but expected");
        }

        private int GetCurrentVoteCount()
        {           
            var currentVoteCount = CurrentVoteCount.Text.Split(":")[1];
            return int.Parse(currentVoteCount.TrimStart().TrimEnd());
        }

        private int GetCurrentCommentListCount()
        {
            return CommentList.Count;
        }
    }
}
