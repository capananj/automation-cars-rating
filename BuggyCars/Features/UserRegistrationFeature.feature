Feature: UserRegistrationFeature
	As a user
	I want to enter my registration details
	So that I can verify that user can register successfully

@UserRegistration
Scenario: Verify that user can register successfully
	Given I navigate to the Buggy cars page
	When I click Register button
	And I enter a unique login id
	And I enter user registration details
		| fieldName        | userInput        |
		| FirstName        | Boogie           |
		| LastName         | Wonder           |
		| Password         | BoogieWonder123! |
		| Confirm Password | BoogieWonder123! |
	And I click Register button to submit user details
	Then I verify that user is registered successfully
