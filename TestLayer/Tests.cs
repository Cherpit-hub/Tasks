using BusinessLayer.PageObjects;
using OpenQA.Selenium;
using RestSharp;
using System.Collections.ObjectModel;
using static CoreLayer.BrowserUtils;

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
            SetLogLevel("DEBUG");
        }

        [Theory]
        [InlineData("C#", "Poland")]
        [InlineData("Java", "Ukraine")]
        public void Task1ValidateThatUserCanSearchForaPositionBasedOnCriteria(string programminglanguage, string country)
        {
            try
            {
                Log.Info($"Starting test Task1 with programming language: {programminglanguage} and country: {country}");
                InitializeWebDriver();
                NavigateToMainPage();
                NavigateToCareersPage();
                NavigateToJobSearchPage();
                _jobSearchPage.EnterProgrammingLanguageIntoSearchField(programminglanguage);
                _jobSearchPage.SelectCountry(country);
                _jobSearchPage.ClickRemotePositionRadioButton();
                _jobSearchPage.ClickJobSearchButton();
                Assert.Contains(programminglanguage, _jobSearchPage.FindRelevantJobOffer(programminglanguage).Text);
            }
            catch (Exception)
            {
                TakeBrowserScreenshot(_driver);
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
                InitializeWebDriver();
                NavigateToMainPage();
                _mainPage.ClickSearchButton();
                _mainPage.EnterSearchQuery(searchQuery);
                ClickFindButton();
                Assert.Equal(ResultsThatContainSearchKeyWord(searchQuery, _searchResultPage.GetSearchResults()).Count(), _searchResultPage.GetSearchResults().Count);
                Log.Info($"Search results validated successfully for search query: {searchQuery}");
            }
            catch (Exception)
            {
                TakeBrowserScreenshot(_driver);
                Log.Warn("Test Task2 failed, screenshot taken for debugging.");
                throw;
            }
        }
        private void ClickFindButton()
        {
            _searchResultPage = _mainPage.ClickSubmitSearchButton();
        }
        private static IEnumerable<IWebElement> ResultsThatContainSearchKeyWord(string searchQuery, ReadOnlyCollection<IWebElement> elements)
        {
            Log.Info($"Filtering search results for keyword: {searchQuery}");
            IEnumerable<IWebElement> filteredElements =
            from element in elements
            where element.Text.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
            select element;
            Log.Info($"Found {filteredElements.Count()} search results that contain the keyword: {searchQuery}");
            return filteredElements;
        }
        [Theory]
        [InlineData("Code-Of-Conduct_01_26.pdf")]
        public void Task3ValidateDownloadFunctionWorksAsExpected(string nameOfFile)
        {
            try
            {
                InitializeWebDriver();
                NavigateToMainPage();
                _mainPage.ScrollToFooter();
                _mainPage.ClickCodeOfConductLink();
                Assert.True(IsFileDownloaded(nameOfFile, _driver));
                Log.Info($"File download validated successfully for file: {nameOfFile}");
            }
            catch (Exception)
            {
                TakeBrowserScreenshot(_driver);
                Log.Warn("Test Task3 failed unexpectedly, screenshot taken for debugging.");
                throw;
            }
        }

        [Fact]
        public void Task4ValidatetitleOfInsightArticleMatchesWithTitleOnCarousel()
        {
            ReadOnlyCollection<string> expectedTitles;
            string actualTitle;
            try
            {
                InitializeWebDriver();
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
            catch (Exception)
            {
                TakeBrowserScreenshot(_driver);
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