Feature: JobSearch

A user should be able to navigate from the main page
of website to the job search page and able to search for desired position

@tag1
Scenario Outline: Searching for a job
	Given User is on the homepage
	And User clicks on 'Careers' link
	Then User clicks 'Start your search here' button on 'Careers' page
	When User enters '<Programming language>' into the 'search by role or keyword' field
	And User selects '<Country>' in 'choose your country' field
	And User selects 'Remote' option
	And User clicks 'Search' button
	Then The latest element of the list should contain '<Programming language>'.
	Examples: 
	| Programming language | Country |
	| C#                   | Poland  |
	| Java                 | Ukraine |
