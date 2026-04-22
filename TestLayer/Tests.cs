using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using BusinessLayer.PageObjects;
using CoreLayer;

namespace TestLayer
{
    public class Tests : BaseTest
    {
        private CareersPage _careersPage = null!;
        private JobSearchPage _jobSearchPage = null!;
        private SearchResultPage _searchResultPage = null!;
        private InsightPage _insightPage = null!;
        private InsightArticlePage _insightArticlePage = null!;
        public Tests() : base()
        {
            SetLogLevel("INFO"); // Set log level to INFO by default, can be overridden by passing a different level as an argument
        }

        [Theory]
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
    }
}