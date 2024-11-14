Feature: ZipCodes

This feature is created to validate main functionality for zip codes endpoints

@positive
Scenario: Get all available zip codes
	When I get all zip codes with "201" response code
		
@positive
Scenario: Expand available zip codes
	When Send request to expand zip code with "81568"
	Then I should have zip code "81568" available

@positive
Scenario: Dublicate zip codes in payload are added as a single one
	Given zip-code payload "expandDuplicateZipCodes.txt" created
	When Send request to expand zip code with payload
	Then I get "201" response code after payload
	And I should have zip code "03289" available
	And there are no zip code "03289" dublicates in available

@positive
Scenario: New zip codes from payload are added but duplicated are not
	Given zip-code payload "ExpandAlreadyUsedZipCodes.txt" created
	When Send request to expand zip code with payload
	Then I get "201" response code after payload
	And I should have zip code "new1" available
	And there are no dublicates in available zip codes


#Scenario #4 
#Given I am authorized user 
#When I send POST request to /zip-codes/expand endpoint 
#And Request body contains list of zip codes 
#And List of zip codes has duplications for already used zip codes 
#Then I get 201 response code 
#And Zip codes from request body are added to available zip codes of application 
#And There are no duplications between available zip codes and already used zip codes 

#Scenario #1 
#Given I am authorized user 
#When I send GET request to /zip-codes endpoint 
#Then I get 200 response code 
#And I get all available zip codes in the application for now 

#Scenario #2 
#Given I am authorized user 
#When I send POST request to /zip-codes/expand endpoint 
#And Request body contains list of zip codes 
#Then I get 201 response code 
#And Zip codes from request body are added to available zip codes of application 

#Scenario #3 
#Given I am authorized user 
#When I send POST request to /zip-codes/expand endpoint 
#And Request body contains list of zip codes 
#And List of zip codes has duplications for available zip codes 
#Then I get 201 response code 
#And Zip codes from request body are added to available zip codes of application 
#And There are no duplications in available zip codes 

