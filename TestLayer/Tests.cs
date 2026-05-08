using BusinessLayer.PageObjects;
using CoreLayer;
using CoreLayer.API;
using CoreLayer.API.Models;
using CoreLayer.Webdriver;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using System.Collections.ObjectModel;

namespace TestLayer
{
    public class Tests : BaseTest
    {
        private CareersPage _careersPage = null!;
        private JobSearchPage _jobSearchPage = null!;
        private SearchResultPage _searchResultPage = null!;
        private InsightPage _insightPage = null!;
        private InsightArticlePage _insightArticlePage = null!;
        private readonly BaseClient _apiClient = new("https://jsonplaceholder.typicode.com");
        public Tests() : base()
        {
            SetLogLevel("INFO"); // Set log level to INFO by default, can be overridden by passing a different level as an argument
        }

        [Theory]
        [Trait("Category", "UI")]
        [InlineData("C#", "Poland")]
        [InlineData("Java", "Ukraine")]
        public void Task1ValidateThatUserCanSearchForaPositionBasedOnCriteria(string programminglanguage, string country)
        {
            try
            {
                Log.Info($"Starting test Task1 with programming language: {programminglanguage} and country: {country}");
                NavigateToMainPage();
                NavigateToCareersPage();
                NavigateToJobSearchPage();
                _jobSearchPage.EnterProgrammingLanguageIntoSearchField(programminglanguage);
                _jobSearchPage.ClearCountryField();
                _jobSearchPage.SelectCountry(country);
                _jobSearchPage.ClickRemotePositionRadioButton();
                _jobSearchPage.ClickSearchButton();
                Assert.Contains(programminglanguage, _jobSearchPage.FindRelevantJobOffer(programminglanguage).Text);
            }
            catch (Xunit.Sdk.ContainsException ex)
            {
                BrowserUtils.TakeBrowserScreenshot(_driver);
                Log.Error($"Test Task1 failed with programming language: {programminglanguage} and country: {country}, Exception message: {ex.Message}");
                throw;
            }
            catch (Exception)
            {
                BrowserUtils.TakeBrowserScreenshot(_driver);
                Log.Warn("Test Task1 failed unexpectedly, screenshot taken for debugging.");
                throw;
            }

        }
        private void NavigateToMainPage()
        {
            Log.Info("Navigating to main page");
            _mainPage = new MainPage(_driver);
        }
        private void NavigateToCareersPage()
        {
            Log.Info("Navigating to careers page");
            _careersPage = _mainPage.ClickCareersLink();
        }
        private void NavigateToJobSearchPage()
        {
            Log.Info("Navigating to job search page");
            _jobSearchPage = _careersPage.ClickJobSearchPageButton();
        }

        [Theory]
        [Trait("Category", "UI")]
        [InlineData("BLOCKCHAIN")]
        [InlineData("Cloud")]
        [InlineData("Automation")]
        public void Task2ValidateGlobalSearchWorksAsExpected(string searchQuery)
        {
            try
            {
                Log.Info($"Starting test Task2 with search query: {searchQuery}");
                NavigateToMainPage();
                _mainPage.ClickSearchButton();
                _mainPage.EnterSearchQuery(searchQuery);
                ClickFindButton();
                Assert.Equal(ResultsThatContainSearchKeyWord(searchQuery, _searchResultPage.GetSearchResults()).Count(), _searchResultPage.GetSearchResults().Count);
                Log.Info($"Search results validated successfully for search query: {searchQuery}");
            }
            catch (Xunit.Sdk.EqualException ex)
            {
                BrowserUtils.TakeBrowserScreenshot(_driver);
                Log.Error($"Assertion failed with search query: {searchQuery}, Exception message: {ex.Message}");
                throw;
            }
            catch (Exception)
            {
                BrowserUtils.TakeBrowserScreenshot(_driver);
                Log.Warn("Test Task2 failed unexpectedly, screenshot taken for debugging.");
                throw;
            }
        }
        private void ClickFindButton()
        {
            _searchResultPage = _mainPage.ClickSubmitSearchButton();
        }
        private static IEnumerable<IWebElement> ResultsThatContainSearchKeyWord(string searchQuery, ReadOnlyCollection<IWebElement> elements)
        {
            IEnumerable<IWebElement> filteredElements =
            from element in elements
            where element.Text.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
            select element;
            return filteredElements;
        }
        [Theory]
        [Trait("Category", "UI")]
        [InlineData("Code-Of-Conduct_01_26.pdf")]
        public void Task3ValidateDownloadFunctionWorksAsExpected(string nameOfFile)
        {
            try
            {
                NavigateToMainPage();
                _mainPage.ScrollToFooter();
                _mainPage.ClickCodeOfConductLink();
                Assert.True(IsFileDownloaded(nameOfFile));
                Log.Info($"File download validated successfully for file: {nameOfFile}");
            }
            catch (Xunit.Sdk.TrueException ex)
            {
                BrowserUtils.TakeBrowserScreenshot(_driver);
                Log.Error($"Assertion failed for file: {nameOfFile}, Exception message: {ex.Message}");
                throw;
            }
            catch (Exception)
            {
                BrowserUtils.TakeBrowserScreenshot(_driver);
                Log.Warn("Test Task3 failed unexpectedly, screenshot taken for debugging.");
                throw;
            }
        }
        public bool IsFileDownloaded(string fileName)
        {
            var downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            var filePath = Path.Combine(downloadPath, fileName);
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10))
            {
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            return wait.Until(d =>
            {
                try
                {
                    return File.Exists(filePath);
                }
                catch (Exception ex)
                {
                    if (ex is IOException || ex is UnauthorizedAccessException)
                    {
                        return false;
                    }
                    else throw;
                }
            });
        }
        [Fact]
        [Trait("Category", "UI")]
        public void Task4ValidatetitleOfInsightArticleMatchesWithTitleOnCarousel()
        {
            ReadOnlyCollection<string> expectedTitles;
            string actualTitle;
            try
            {
                NavigateToMainPage();
                NavigateToInsightPage();
                _insightPage.ClickCarouselRightButton();
                expectedTitles = _insightPage.GetInsightArticles();
                Log.Info($"Expected titles retrieved from carousel: {string.Join(", ", expectedTitles)}");
                _insightArticlePage = _insightPage.ClickReadMoreButtonOfFirstInsightArticle();
                actualTitle = _insightArticlePage.GetArticleTitle();
                Log.Info($"Actual title retrieved from article page: {actualTitle}");
                AssertContains(expectedTitles, actualTitle);
            }
            catch (Xunit.Sdk.TrueException ex)
            {
                BrowserUtils.TakeBrowserScreenshot(_driver);
                Log.Error($"Assertion failed for Task4 article title, Exception message: {ex.Message}");
                throw;
            }
            catch (Exception)
            {
                BrowserUtils.TakeBrowserScreenshot(_driver);
                Log.Warn("Test Task4 failed unexpectedly, screenshot taken for debugging.");
                throw;
            }
        }
        private void NavigateToInsightPage()
        {
            _insightPage = _mainPage.ClickInsightLink();
        }
        private static void AssertContains(IEnumerable<string> expectedTitles, string actualTitle)
        {
            bool isTitleFound = false;
            foreach (var title in expectedTitles)
            {
                if (actualTitle.Contains(title, StringComparison.OrdinalIgnoreCase))
                {
                    isTitleFound = true;
                    break;
                }
            }
            Assert.True(isTitleFound, $"Expected title was not found in the actual title. Actual title: {actualTitle}");
        }

        [Theory]
        [Trait("Category", "API")]
        [InlineData(new object[] { "id", "name", "username", "email", "address", "phone", "website", "company" })]
        public async Task Task1ValidateThatListOfUsersWithCorrectFieldsIsReturnedSuccesfully(params string[] expectedFields)
        {
            try
            {
                var response = await _apiClient.GetUsersAsync();
                var UserList = JArray.Parse(response.Content!);
                Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK && ResponseNoErrorMessage(response), $"API call was not successful. Status code: {response.StatusCode}");
                AssertUsersInListContainsExpectedFields(expectedFields, UserList);
                Log.Info("Task1 List of users returned data successfully.");
            }
            catch (Xunit.Sdk.TrueException ex)
            {
                Log.Error($"Assertion failed for Task1 API call not successful, Exception message: {ex.Message}");
                throw;
            }
            catch (Xunit.Sdk.FalseException ex)
            {
                Log.Error($"Assertion failed for Task1 Users do not contain valid information, Exception message: {ex.Message}");
                throw;
            }
            catch (Exception)
            {
                Log.Warn("API Test Task1 failed unexpectedly, check logs for details.");
                throw;
            }
        }

        private static void AssertUsersInListContainsExpectedFields(string[] expectedFields, JArray JsonArray)
        {
            foreach (var user in JsonArray)
            {
                foreach (var field in expectedFields)
                {
                    Assert.True(user[field] != null, $"User does not contain expected field: {field}");
                }
            }
        }

        [Theory]
        [Trait("Category", "API")]
        [InlineData("application/json; charset=utf-8")]
        public async Task Task2ValidateResponseHeaderForAListOfUsers(string expectedContentType)
        {
            try
            {
                var response = await _apiClient.GetUsersAsync();

                Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK && ResponseNoErrorMessage(response), $"API call was not successful. Status code: {response.StatusCode}, Error message: {response.ErrorMessage}");
                Assert.True(response.ContentHeaders?.Any(h => h.Name.Equals("Content-Type", StringComparison.OrdinalIgnoreCase)), "Response does not contain expected Content-Type header.");
                Assert.True(response.ContentHeaders?.Any(h => h.Value.ToString().Contains(expectedContentType)), $"Response does not contain expected Content-Type header with value: {expectedContentType}");
                Log.Info("Task2 Response header for list of users validated successfully.");
            }
            catch (Xunit.Sdk.TrueException ex)
            {
                Log.Error($"Assertion failed for Task2 response header validation, Exception message: {ex.Message}");
                throw;
            }
            catch (Exception)
            {
                Log.Warn("API Test Task2 failed unexpectedly, check logs for details.");
                throw;
            }
        }
        [Theory]
        [InlineData(10)]
        [Trait("Category", "API")]
        public async Task Task3ValidateThatListOfUsersFieldsContainValidInformation(int expectedCount)
        {
            try
            {
                var response = await _apiClient.GetUsersAsync();
                var duplicateIds = response.Data!
                .GroupBy(u => u.Id)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

                Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK && ResponseNoErrorMessage(response), $"API call was not successful. Status code: {response.StatusCode}");
                Assert.True(response.Data!.Count == expectedCount, $"Expected {expectedCount} users, but got {response.Data.Count}.");
                Assert.True(duplicateIds.Count == 0, $"Duplicate user IDs found: {string.Join(", ", duplicateIds)}");
                AssertUsersContainValidInformation(response);
                Log.Info("Task3 List of users fields contain valid information validated successfully.");
            }
            catch (Xunit.Sdk.TrueException ex)
            {
                Log.Error($"Assertion failed for Task3 list of users fields contain valid information, Exception message: {ex.Message}");
                throw;
            }
            catch (Xunit.Sdk.FalseException ex)
            {
                Log.Error($"Assertion failed for Task3 list of users fields contain valid information, Exception message: {ex.Message}");
                throw;
            }
            catch (Exception)
            {
                Log.Warn("API Test Task3 failed unexpectedly, check logs for details.");
                throw;
            }
        }

        [Theory]
        [Trait("Category", "API")]
        [InlineData("Johnson", "Mister")]
        public async Task Task4ValidateThatUserCanBeCreatedSuccessfully(string name, string username)
        {
            try
            {
                var newUser = new User
                {
                    Name = name,
                    Username = username,
                };
                var response = await _apiClient.CreateUserAsync(newUser);

                Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created && ResponseNoErrorMessage(response), $"API call was not successful. Status code: {response.StatusCode}, Error message: {response.ErrorMessage}");
                Assert.True(!string.IsNullOrEmpty(response.Data?.ToString()), "Response data is empty or null.");
                Assert.True(response.Data.Id.HasValue, "Response does not contain a user ID.");
            }
            catch (Xunit.Sdk.TrueException ex)
            {
                Log.Error($"Assertion failed for Task4 user creation, Exception message: {ex.Message}");
                throw;
            }
            catch (Exception)
            {
                Log.Warn("API Test Task4 failed unexpectedly, check logs for details.");
                throw;
            }
        }

        [Fact]
        [Trait("Category", "API")]
        public async Task Task5ValidateThatUserIsNotifiedIfResourceDoesNotExist()
        {
            try
            {
                var response = await _apiClient.GetUsersNotExistingAsync();

                Assert.True(response.StatusCode == System.Net.HttpStatusCode.NotFound && ResponseNoErrorMessage(response), $"Expected status code 404 Not Found, but got {response.StatusCode}");
                Log.Info("Task5 User is notified when resource does not exist validated successfully.");
            }
            catch (Xunit.Sdk.TrueException ex)
            {
                Log.Error($"Assertion failed for Task5 resource not found validation, Exception message: {ex.Message}");
                throw;
            }
            catch (Exception)
            {
                Log.Warn("API Test Task5 failed unexpectedly, check logs for details.");
                throw;
            }
        }
        private static bool ResponseNoErrorMessage(RestResponse response)
        {
            return string.IsNullOrEmpty(response.ErrorMessage);
        }

        private static void AssertUsersContainValidInformation(RestResponse<List<User>> response)
        {
            response.Data?.ForEach(user =>
            {
                Assert.False(string.IsNullOrEmpty(user.Name), "User name should not be null or empty.");
                Assert.False(string.IsNullOrEmpty(user.Username), "User username should not be null or empty.");
                Assert.False(user.Company == null, "User company should not be null.");
                Assert.False(string.IsNullOrEmpty(user.Company.Name), "User company name should not be null or empty.");
            });
        }
    }
}