Feature: UserProfileFeature
	As a user
	I want to modify the user profile
	So that I can verify user can update their profile details

@UserProfile
Scenario Outline: Verify user can modify profile details successfully
	Given I navigate to the Buggy cars page
	And I enter username and password
	And I click Login button
	When I click Profile link
	And I enter user details Firstname: <Firstname>, Lastname: <Lastname>, Gender: <Gender>, Age: <Age>, Address: <Address>, Phone: <Phone>, Hobby: <Hobby>
	And I click Save button
	Then I verify that profile has been updated successfully

	Examples: 
	| Firstname | Lastname | Gender | Age | Address      | Phone     | Hobby  |
	| Walter    | Doe      | Male   | 25  | North Dakota | 123123123 | Hiking |
	