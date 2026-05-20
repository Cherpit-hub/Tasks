Feature: FileDownload

User should be able to download the downloadable pdf files on the pages

@tag1
Scenario: Code of conduct download
	Given I am on the homepage
	And I scroll down to the footer
	When I click on Code of Ethical conduct (PDF) in Policies section
	Then The file "Code-Of-Conduct_01_26.pdf" should be downloaded
