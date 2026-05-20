Feature: FileDownload

User should be able to download the downloadable pdf files on the pages

@tag1
Scenario: Code of conduct download
	Given User is on the homepage
	And User scrolls down to the footer
	When User clicks on 'Code of Ethical conduct' in policies section
	Then Verify that the file "Code-Of-Conduct_01_26.pdf" is downloaded
