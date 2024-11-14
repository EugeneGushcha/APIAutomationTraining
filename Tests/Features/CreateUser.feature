Feature: CreateUser

Create a new user

@positive
Scenario: Create a new user with valid inputs
	Given User payload "CreateUserValidData.json"
	When Send request to create user
	Then Validate user is created


#@positive
#Scenario: Get zip codes
#	Given I get all zip codes

	
@positive
Scenario: Get available users
	Given I get all users



#Scenario: Create a new user with invalid inputs
#	Given I am authorized user 
#	When I send GET request to zip-codes endpoint 
#	Then I get 200 response code 
#	And I get all available zip codes in the application for now 
