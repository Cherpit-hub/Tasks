using OpenQA.Selenium;

namespace BusinessLayer.PageObjects
{
    public class CareersPage : BasePage
    {
        readonly By startButtonLocator = By.ClassName("pinned-button-ui-23");
        public CareersPage(IWebDriver driver) : base(driver)
        {
        }
        public JobSearchPage ClickJobSearchPageButton()
        {
            FindElement(startButtonLocator).Click();
            return new JobSearchPage(_driver);
        }
    }
}
