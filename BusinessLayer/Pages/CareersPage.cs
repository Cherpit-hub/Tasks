using OpenQA.Selenium;

namespace BusinessLayer.PageObjects
{
    public class CareersPage : BasePage
    {
        readonly By startButtonLocator = By.CssSelector("div[class*=button] div[data-gtm-category *= job_search]");
        public CareersPage(IWebDriver driver) : base(driver)
        {
        }
        public JobSearchPage ClickJobSearchPageButton()
        {
            Log.Info("Clicking the 'Start' button to navigate to the Job Search Page.");
            FindElement(startButtonLocator).Click();
            Log.Info("'Start' button clicked successfully, navigating to Job Search Page.");
            return new JobSearchPage(_driver);
        }
    }
}
