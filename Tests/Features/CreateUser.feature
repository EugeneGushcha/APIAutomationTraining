Feature: CreateUser

Create a new user

@positive
Scenario: Create a new user with valid inputs
	Given User payload "CreateUserValidData.json"
	When Send request to create user
	Then Validate user is created

	
@positive
Scenario: Get available users
	Given I get all users


