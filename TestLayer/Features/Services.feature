Feature: Validation of Navigation to services

A short summary of the feature

@tag1
Scenario Outline: Navigation to services options
	Given I am on the homepage
	When I click on the Services link
	And Click on the '<service_name>' link
	Then It should have the correct title
	And have section Our related Expertise displayed on the page
	Examples:
	| service_name   |
	| Generative AI  |
	| Responsible AI |