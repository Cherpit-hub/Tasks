Feature: InsightsCarousel

Site contains carousel that spins and changes to different links periodically


@tag1
Scenario: Title of article matches with title in carousel
	Given User is on the homepage
	And User clicks 'Insights' on the top menu
	And User swipes carousel from two to five more timesUser swipes carousel from two to five more times
	When User notes the name of article
	And User clicks on 'Read More' button
	Then The name of article should match with the one on the carousel
