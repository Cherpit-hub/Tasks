Task #1
Implement the automated test using the SpecFlow BDD framework.
Precondition:
Execute test case manually before creating automated test. Make the test parametrized.
Test case #1. Validate Navigation to Services Section
Navigate to https://www.epam.com/
Locate and click on the "Services" link in the main navigation menu.
From the dropdown, select a specific service category: “Generative AI” or “Responsible AI” (parameterize the category selection).
Validate that the page contains the correct title.
Validate that the section ‘Our Related Expertise’ is displayed on the page


Task #2
Enhance an existing solution based on the Page Object model using a BDD approach. This task requires you to organize your project with a clear and logical folder structure and to implement required classes for a fully functioning BDD test suite.
Precondition: 
Make sure you have Visual Studio (or a similar IDE), Selenium WebDriver, SpecFlow, and a compatible SpecFlow runner installed on your machine.
Before proceeding with the BDD implementation, execute the existing tests under the Page Object model structure. Ensure that all tests are passing as this serves as your base functionality upon which you will build the BDD solution.
Task description:
Folder Structure Set-up:
Create a structured folder hierarchy in your project to segregate different components like features, step definitions and page objects.
Ensure the folders reflect a logical and modular test architecture.

Class Incorporation:
Add necessary classes to the respective project folders. This may include addition of new Page Objects, Step Definition files.
Make sure all classes are correctly named and reside in appropriate folders based on their functionality.

BDD Feature File Development:
In a .feature file within your Features folder, describe your test scenario using the BDD syntax: Given, When, Then.
Clearly outline the preconditions, actions, and expected outcomes for the tests.

Page Objects Refactoring:
Adapt your Page Object classes to align with BDD steps. Ensure these objects remain modular and avoid any direct test assertions within these classes.


