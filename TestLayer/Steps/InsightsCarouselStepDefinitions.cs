using BusinessLayer.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using System;
using System.Collections.ObjectModel;

namespace TestLayer.Steps
{
    [Binding]
    public class InsightsCarouselStepDefinitions
    {
        private IWebDriver _driver;
        private MainPage _mainPage;
        private InsightPage _insightPage = null!;
        private InsightArticlePage _insightArticlePage = null!;
        ReadOnlyCollection<string> expectedTitles = null!;

        public InsightsCarouselStepDefinitions(ScenarioContext scenarioContext)
        {
            _driver = (IWebDriver)scenarioContext["WebDriver"];
            _mainPage = (MainPage)scenarioContext["MainPage"];
        }

        [Given("User clicks 'Insights' on the top menu")]
        public void GivenUserClicksInsightsOnTheTopMenu()
        {
            NavigateToInsightPage();
        }

        [Given("User swipes carousel two or more times")]
        public void GivenUserSwipesCarouselTwoOrMoreTimes()
        {
            _insightPage.ClickCarouselRightButton();
        }

        [When("User notes the name of article")]
        public void WhenUserNotesTheNameOfArticle()
        {
            expectedTitles = _insightPage.GetInsightArticles();
        }

        [When("User clicks on 'Read More' button")]
        public void WhenUserClicksOnReadMoreButton()
        {
            ClickReadMoreButton();
        }

        [Then("The name of article should match with the one on the carousel")]
        public void ThenTheNameOfArticleShouldMatchWithTheOneOnTheCarousel()
        {
            AssertContains(expectedTitles, _insightArticlePage.GetArticleTitle());
        }
        
        private void NavigateToInsightPage()
        {
            _insightPage = _mainPage.ClickInsightLink();
        }
        private void ClickReadMoreButton()
        {
            _insightArticlePage = _insightPage.ClickReadMoreButtonOfFirstInsightArticle();
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
