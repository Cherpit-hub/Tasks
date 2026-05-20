using CoreLayer;
using OpenQA.Selenium;

namespace BusinessLayer.PageObjects
{
    public class MainPage : BasePage
    {
        readonly By _careersLocator = By.LinkText("Careers");
        readonly By _codeOfConductLocator = By.XPath("//div[contains(@class,'footer')]//a[contains(text(),'Ethical')]");
        readonly By _insightLocator = By.PartialLinkText("Insight");
        public MainPage(IWebDriver driver) : base(driver)
        {
            NavigateTo(Configuration.AppUrl);
        }
        public CareersPage ClickCareersLink()
        {
            FindElement(_careersLocator).Click();
            return new CareersPage(_driver);
        }
        public void ClickCodeOfConductLink()
        {
            FindElement(_codeOfConductLocator).Click();
        }
        public void ScrollToFooter()
        {
            ScrollToElement(_codeOfConductLocator);
        }
        public InsightPage ClickInsightLink()
        {
            FindElement(_insightLocator).Click();
            return new InsightPage(_driver);
        }
    }
}
