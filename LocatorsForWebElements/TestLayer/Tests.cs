using LocatorsForWebElements.BusinessLayer.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using LocatorsForWebElements.CoreLayer;
namespace LocatorsForWebElements.TestLayer
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
        }

        [Theory]
        [InlineData("C#", "Poland")]
        [InlineData("Java", "Ukraine")]
        public void Task1ValidateThatUserCanSearchForaPositionBasedOnCriteria(string programminglanguage, string country)
        {
            try
            {
                WebDriverFactory.CreateWebDriver(WebDriverFactory.BrowserType.Chrome);
                NavigateToMainPage();
                NavigateToCareersPage();
                NavigateToJobSearchPage();
                _jobSearchPage.EnterProgrammingLanguageIntoSearchField(programminglanguage);
                _jobSearchPage.ClearCountryField();
                _jobSearchPage.SelectCountry(country);
                _jobSearchPage.ClickRemotePositionRadioButton();
                _jobSearchPage.ClickSearchButton();
                Assert.Contains(programminglanguage, _jobSearchPage.FindRelevantJobOffer(programminglanguage).Text);
                _driver.Quit();
            }
            catch (Exception)
            {
                _driver.Quit();
                throw;
            }

        }
        private void NavigateToMainPage()
        {
            _mainPage = new MainPage(_driver);
        }
        private void NavigateToCareersPage()
        {
            _careersPage = _mainPage.ClickCareersLink();
        }
        private void NavigateToJobSearchPage()
        {
            _jobSearchPage = _careersPage.ClickJobSearchPageButton();
        }

        [Theory]
        [InlineData("BLOCKCHAIN")]
        [InlineData("Cloud")]
        [InlineData("Automation")]
        public void Task2ValidateGlobalSearchWorksAsExpected(string searchQuery)
        {
            //* PartialLinkText
            try
            {
                WebDriverFactory.CreateWebDriver(WebDriverFactory.BrowserType.Chrome);
                NavigateToMainPage();
                _mainPage.ClickSearchButton();
                _mainPage.EnterSearchQuery(searchQuery);
                ClickFindButton();
                Assert.Equal(ResultsThatContainSearchKeyWord(searchQuery, _searchResultPage.GetSearchResults()).Count(), _searchResultPage.GetSearchResults().Count);
                _driver.Quit();
            }
            catch (Exception)
            {
                _driver.Quit();
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
                WebDriverFactory.CreateWebDriver(WebDriverFactory.BrowserType.Chrome);
                NavigateToMainPage();
                _mainPage.ScrollToFooter();
                _mainPage.ClickCodeOfConductLink();
                Assert.True(IsFileDownloaded(nameOfFile));
                _driver.Quit();
            }
            catch (Exception)
            {
                _driver.Quit();
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
                WebDriverFactory.CreateWebDriver(WebDriverFactory.BrowserType.Chrome);
                NavigateToMainPage();
                NavigateToInsightPage();
                _insightPage.ClickCarouselRightButton();
                expectedTitles = _insightPage.GetInsightArticles();
                _insightArticlePage = _insightPage.ClickReadMoreButtonOfFirstInsightArticle();
                actualTitle = _insightArticlePage.GetArticleTitle();
                AssertContains(expectedTitles, actualTitle);
                _driver.Quit();
            }
            catch (Exception)
            {
                _driver.Quit();
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
        //    _driver.Navigate().GoToUrl("chrome://downloads/");
        //    var _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3))
        //    {
        //        PollingInterval = TimeSpan.FromMilliseconds(500)
        //    };
        //    _wait.Until(d =>
        //    {
        //        try
        //        {
        //            var shadowRoot = _driver.FindElement(By.CssSelector("body > downloads-manager"))
        //                .GetShadowRoot().FindElement(By.CssSelector("#list>"))
        //                .GetShadowRoot().FindElement(By.CssSelector("#fileLink"));
        //            return shadowRoot.Text.Contains(fileName);
        //        }
        //        catch (Exception ex)
        //        {
        //            if (ex is StaleElementReferenceException || ex is NoSuchElementException)
        //            {
        //                return false;
        //            }
        //            else throw;
        //        }
        //    });
        //    return true;
        //}
    }
}