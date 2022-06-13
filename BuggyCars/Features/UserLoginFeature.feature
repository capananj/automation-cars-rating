Feature: UserLoginFeature
	As a user
	I want to able to enter username and password
	So that I can verify that only valid credentials can login successfully 

Background: 
	Given I navigate to the Buggy cars page	

@UserLogin
Scenario: Verify user with valid credentials can login successfully
	When I enter username and password
	And I click Login button
	Then I verify user is able to login successfully

@UserLogin
Scenario: Verify user with invalid credentials can login successfully
	When I enter username Dummy-012345 and password Dummy-012345
	And I click Login button
	Then I verify user is not able to login successfully


