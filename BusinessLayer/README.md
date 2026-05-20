For this practical task the result of the previous module should be used. 
The solution should be split into next layers:
Core layer (core functionality of TAF, that aren’t project specific).
Business layer (should contain all functionality, related with business logic of the tested application.
Tests layer (should contain automated tests, TAF configuration).

Initialization of WebDriver instance should be done with helping Factory design pattern (consider adding dedicated class for Browser Factory functionality) 
All repeating actions, that should be done from test to test, should be aggregated in an abstract base class (actions such as initialization and closing a browser and so on).
Logging should be implemented (NLog, log4net or Serilog). Each test should generate logs from what should be clear test actions. TAF should support logging to both, file and console, and opportunity to use different logging levels (Error, Info, etc.). Min log level should be configurable via TAF configuration. When the test fails, a screenshot with the date and time should be taken. 


Tasks #1-#4:
Refactor automated tests created in previous module to follow SOLID, DRY, KISS, YAGNI principles, use Design Patterns (Singleton and Browser Factory) and add logging mechanism.  Solution should have Layers in Architecture and be able to execute on several environments.
