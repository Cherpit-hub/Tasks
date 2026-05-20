Feature: Validation of Navigation to services

A short summary of the feature

@tag1
Scenario Outline: Navigation to services options
	Given User is on the homepage
	When User clicks on 'Services' link
	And User clicks on the '<Service name>' link
	Then It should have the correct title
	And Have section 'Our Related Expertise' displayed on the page
	Examples:
	| Service name   |
	| Generative AI  |
	| Responsible AI |