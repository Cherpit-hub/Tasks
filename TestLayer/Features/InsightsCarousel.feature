Feature: InsightsCarousel

Site contains carousel that spins and changes to different links periodically


@tag1
Scenario: Title of article matches with title in carousel
	Given I am on the homepage
	And I Click Insights from the top menu
	And I Swipe carousel two or more times
	When I note the name of article
	And I click on Read More button
	Then The name of article should match with the one on the carousel
