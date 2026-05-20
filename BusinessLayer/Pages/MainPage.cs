using CoreLayer;
using OpenQA.Selenium;

namespace BusinessLayer.PageObjects
{
    public class MainPage : BasePage
    {
        readonly By _codeOfConductLocator = By.XPath("//div[contains(@class,'footer')]//a[contains(text(),'Ethical')]");
        readonly By _insightLocator = By.PartialLinkText("Insight");
        public MainPage(IWebDriver driver) : base(driver)
        {
            NavigateTo(Configuration.AppUrl);
            Log.Info("Navigated to main page");
        }
        public void ClickCodeOfConductLink()
        {
            FindElement(_codeOfConductLocator).Click();
            Log.Info("Clicked on Code of Ethical Conduct link");
        }
        public void ScrollToFooter()
        {
            ScrollToElement(_codeOfConductLocator);
            Log.Info("Scrolled to footer");
        }
        public InsightPage ClickInsightLink()
        {
            FindElement(_insightLocator).Click();
            Log.Info("Clicked on Insight link");
            return new InsightPage(_driver);
        }
    }
}
