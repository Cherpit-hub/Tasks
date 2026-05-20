Feature: GlobalSearch

Users should be able to use the global search to search for 
things across all website

@tag1
Scenario Outline: Global search results contain search word
	Given User is on the homepage
	And User clicks on magnifier icon
	When User fills the search field with '<Query>'
	And User clicks on Find button
	Then In the list of results all links should contain a '<Query>' in the text
	Examples: 
	| Query      |
	| BLOCKCHAIN |
	| Cloud      |
	| Automation |