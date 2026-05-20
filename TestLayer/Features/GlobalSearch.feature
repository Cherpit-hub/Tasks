Feature: GlobalSearch

Users should be able to use the global search to search for 
things across all website

@tag1
Scenario Outline: Global search results contain search word
	Given I am on the homepage
	And I Click on magnifier icon
	When I fill the search field with '<Query>'
	And Click Find button
	Then In the list of results all links should contain a '<Query>' in the text
	Examples: 
	| Query      |
	| BLOCKCHAIN |
	| Cloud      |
	| Automation |