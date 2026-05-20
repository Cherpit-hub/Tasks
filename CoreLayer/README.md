For this practical task, Selenium WebDriver and any preferred by you unit test framework (NUnit, MSTest or xUnit) should be used. 
Basic WebDriver features as browser interactions, capabilities and features browser specifics, explicit and implicit waits should be used. 
Read website URL from config file as a property. Enlarge the window. Use basic commands that can be executed on an element (at least click, sendKeys, clear). 
The solution should contain only one class with several test methods. The following locators MUST be used at least once:
• ID locator
• Name locator
• ClassName locator
• TagName locator
• LinkText locator
• PartialLinkText locator
• CSS locator (if possible, use pseudo-classes)
• XPath locator (Relative path)
• XPath locator with any operator
• XPath locator with axes
Test tasks
Precondition: Execute tests cases manually before creating automated tests. Make all tests parameterized to practice Data Driven approach


Task #1. Validate that user can search for a position based on criteria
Create a Chrome instance. · Navigate to https://www.epam.com/
Find a link “Carriers” and click on it
On the Careers page, click “Start your search here”
Enter any programming language name in the field “Search by Role or Keyword” (should be taken from test parameter)
Select a value in “Choose your country” field (should be taken from test parameter)
Select option “Remote”
Click on the button “Search”
Find the latest element in the list of results
Expand the element and validate that programming language from search is present
Close the browser



Task #2. Validate global search works as expected
Create a Chrome instance. 
Navigate to https://www.epam.com/
Find a magnifier icon and click on it
Find a search string and put there “BLOCKCHAIN”/”Cloud”/”Automation” (use as a parameter for a test)
Click “Find” button
Validate that all links in a list contain a word “BLOCKCHAIN”/”Cloud”/”Automation” in the text. LINQ should be used.
Close the browser



Tasks #1 & #2:
Refactor tasks created for previous module to use PageObject pattern to abstract any page information away from the actual.
Precondition: Execute tests cases manually before creating automated tests. Make all tests parameterized to practice Data Driven approach.


Task #3. Validate file download function works as expected:
Create a Chrome instance 2. Navigate to https://www.epam.com/
Scroll down to the page footer
Click on “Code of Ethical Conduct (PDF)” in “Policies” section
Wait till file is downloaded
Validate that file “Code-Of-Conduct_01_26.pdf” downloaded (use name of the file as a parameter)
Close the browser

Task #4. Validate title of the article matches with title in carousel:
Create a Chrome instance 2. Navigate to https://www.epam.com/
Select “Insights” from the top menu
Swipe a carousel two or more times
Note the name of article
Click on “Read More” (or similar) button
Validate that the name of article matches with the noted above
Close the browsers.
