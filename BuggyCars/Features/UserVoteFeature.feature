Feature: UserVoteFeature
	As a user
	I want to submit a vote to any car make or model
	So that I can verify that only registered user can submit a vote

Background: 
	Given I navigate to the Buggy cars page	

@UserVoteFeature
Scenario Outline: Verify that registered user can submit a vote
	Given I click Register button
	And I enter a unique login id
	And I enter user registration details
		| fieldName        | userInput     |
		| FirstName        | Scooby        |
		| LastName         | Doo           |
		| Password         | ScoobyDoo123! |
		| Confirm Password | ScoobyDoo123! |
	And I click Register button to submit user details
	And I verify that user is registered successfully
	And I enter username and password
	And I click Login button
	When I view the list of all registered model
	And I click View more link on any make and model
	And I enter comment <comment>
	And I click Vote button
	Then I verify that vote is submitted successfully
	And I verify vote count has incremented
	And I verify that comment <condition> displayed in the table

	Examples:
	| condition | comment            |
	| is        | One of the beast!! |
	| is not    |                    |

@UserVoteFeature
Scenario: Verify that unregistered user cannot submit a vote
	When I view the list of all registered model
	And I click View more link on any make and model
	Then I verify that unregistered user cannot submit a vote