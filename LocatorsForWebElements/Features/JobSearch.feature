Feature: JobSearch

A user should be able to navigate from the main page
of website to the job search page and able to search for desired position

@tag1
Scenario Outline: Searching for a job
	Given I am on the homepage
	And I click on Careers link
	When T click Start your search here on Careers page
	Then Jobs page should load
	When I Enter '<Programming language>' into the Search by role or keyword field
	And I Select '<Country>' in Choose your country field
	And I Select Remote option
	And Click Search button
	Then The latest element of the list should contain '<Programming language>'.
	Examples: 
	| Programming language | Country |
	| C#                   | Poland  |
	| Java                 | Ukraine |
