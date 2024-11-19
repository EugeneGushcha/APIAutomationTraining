Feature: CreateUser

Create a new user

@positive
Scenario: Create a new user with valid inputs
	Given CreateUser payload "CreateUserValidData.json" created
	When Send request to create user
	Then I get "201" response code after CreateUser payload 
	And Validate user with name "Tanya" age "52" sex "FEMALE" and zip code "54321" is created
	Then I should not have zip code "54321" available

	
@positive
Scenario: Get available users
	Given I get all users

@positive
Scenario: 424 Response code returns after trying to use unavailable zip code
	Given CreateUser payload "CreateUserUnavailableZipCode.json" created
	When Send request to create user
	Then I get "424" response code after CreateUser payload
	And Validate user with name "Tanya2" age "52" sex "FEMALE" and zip code "645649" is NOT created

@positive
Scenario: 400 Response code returns after trying to create a user with the same name and sex
	Given CreateUser payload "CreateUserValidData.json" created
	When Send request to create user
	Given CreateUser payload "NotUniqueUser.json" created
	When Send request to create user
	Then I get "400" response code after CreateUser payload
	And Validate user with name "Eugene1" age "51" sex "MALE" and zip code "04001" is NOT dublicated

#Scenario #1
#Given I am authorized user
#When I send POST request to /users endpoint
#And Request body contains user to add
#And All fields are filled in
#Then I get 201 response code
#And User is added to application
#And Zip code is removed from available zip codes of application

#Scenario #2                                             [!!! SCIPPED !!!]
#Given I am authorized user
#When I send POST request to /users endpoint
#And Request body contains user to add
#And Required fields are filled in
#Then I get 201 response code
#And User is added to application

#Scenario #3
#Given I am authorized user
#When I send POST request to /users endpoint
#And Request body contains user to add
#And All fields are filled in
#And Zip code is incorrect (unavailable)
#Then I get 424 response code
#And User is not added to application

#Scenario #4
#Given I am authorized user
#When I send POST request to /users endpoint
#And Request body contains user to add with the same name and sex as existing user in the system
#Then I get 400 response code
#And User is not added to application