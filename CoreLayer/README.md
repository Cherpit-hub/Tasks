Please use RestSharp library for creating API automated tests. 


Update TAF adding an opportunity to support API testing:
Update Core layer with the base client. 
Update Business layer with necessary business models.
Tests’ layer should contain the implemented API tests (listed below).



The solution should contain only one class with several test methods for API testing.  All tests should support parallelization mechanism.
Use Builder Design Pattern to construct complex requests step by step.
Logging should be implemented (NLog, log4net or Serilog) for API part of TAF as well. Each test should generate logs from what should be clear test actions. TAF should support logging to both, file and console, and opportunity to use different logging levels (Error, Info, etc.). Min log level should be configurable via TAF configuration. All implemented below tests should be marked with “API” test category.


Tasks #1. Validate that the list of users can be received successfully
Create and send request to https://jsonplaceholder.typicode.com/usersusing GET method
Validate that user recives a list of users with the following information: "id",  "name", "username", "email", "address”,     "phone",   "website",  "company";
Validate that user receives 200 OK response code. There are no error messages;



Tasks #2. Validate response header for a list of users 
Create and send request to https://jsonplaceholder.typicode.com/usersusing GET method.
Validate content-type header exists in the obtained response.
The value of the content-type header is application/json; charset=utf-8.
Validate that user receives 200 OK response code. There are no error messages.

Tasks #3. Validate response header for a list of users 
Create and send request to https://jsonplaceholder.typicode.com/usersusing GET method. 
Validate that the content of the response body is the array of 10 users.
Validate that each user should be with different ID.
Validate that each user should be with non-empty Name and Username.
Validate that each user contains the Company with non-empty Name Validate that user receives 200 OK response code. There are no error messages.

Tasks #4. Validate that user can be created
Create and send request to https://jsonplaceholder.typicode.com/usersusing POST method with Name and Username fields 
Validate that response is not empty and contains the ID value
Validate that user receives 201 Created response code. There are no error messages

Tasks #5. Validate that user is notified if resource doesn’t exist
Create and send a request to https://jsonplaceholder.typicode.com/invalidendpoint using GET method.
Validate that user receives 404 Not Found response code. There are no error messages.  

