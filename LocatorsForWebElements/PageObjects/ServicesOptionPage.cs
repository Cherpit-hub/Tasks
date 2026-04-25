using OpenQA.Selenium;

namespace LocatorsForWebElements.PageObjects
{
    internal class ServicesOptionPage : BasePage
    {
        readonly By _titleLocator = By.CssSelector("span[class *= 'museo-sans-500 gradient-text']");
        readonly By _sectionLocator = By.XPath("//span[contains(text(),'Our Related Expertise')]");
        public ServicesOptionPage(IWebDriver driver) : base(driver)
        {
        }
        public string GetTitle()
        {
            return FindElement(_titleLocator).Text;
        }
        public bool IsSectionDisplayed()
        {
            ScrollToElement(_sectionLocator);
            return FindElement(_sectionLocator).Displayed;
        }
    }
}
