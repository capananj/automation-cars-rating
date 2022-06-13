Feature: ViewListOfCarMakeAndModel
	As a user
	I want to be able to view the list of registered models
	So that I can verify that car make and model table is displayed

Background: 
	Given I navigate to the Buggy cars page	

@ViewListOfCarMakeAndModel
Scenario: Verify that list of car make and model is displayed
	When I view the list of all registered model
	Then I verify that car make and model table is displayed