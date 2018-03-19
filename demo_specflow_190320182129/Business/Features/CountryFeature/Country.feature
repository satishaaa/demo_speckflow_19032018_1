Feature: Country
	API methods related to Country are validated here

@mytag
Scenario: Check that response by calling code and by capital city with 372
	Given Calling a API and I entered Capital : Tallinn and Code : 372
	Then the case should success

@mytag
Scenario: Check that response by calling code and by capital city with 371
	Given Calling a API and I entered Capital : Tallinn and Code : 371
	Then the case should failure

@mytag
Scenario: Find Countries in regions for example for Asia
	Given Calling a API and I entered region : Asia
	Then the system should return bordering countries more than 3

@mytag
Scenario: Find Countries in regions for example for Europe
	Given Calling a API and I entered region : Europe
	Then the system should return bordering countries more than 4

@mytag
Scenario: Check country and expected bordering countries in given region
	Given Calling a API and I entered name : Malta 
	Then the case should success

