using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LocatorsForWebElements.PageObjects
{
    internal class MainPage : BasePage
    {
        private readonly string _url;
        readonly By _careersLocator = By.LinkText("Careers");
        readonly By _magnifiericonLocator = By.ClassName("header__icon");
        readonly By _searchFieldLocator = By.Id("new_form_search");
        readonly By _searchButtonLocator = By.XPath("//button[contains(@class,'custom-button')]");
        readonly By _codeOfConductLocator = By.CssSelector(".policies-right > li:nth-child(5) > a:nth-child(1)");
        readonly By _insightLocator = By.PartialLinkText("Insight");
        readonly By _servicesLocator = By.LinkText("Services");
        public MainPage(IWebDriver driver) : base(driver)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            _url = config["ApplicationUrl"] ?? string.Empty;
            NavigateTo(_url);
        }
        public CareersPage ClickCareersLink()
        {
            FindElement(_careersLocator).Click();
            return new CareersPage(_driver);
        }
        public void ClickMagnifierButton()
        {
            FindElement(_magnifiericonLocator).Click();
        }
        public void EnterSearchQuery(string searchQuery)
        {
            FindElement(_searchFieldLocator).SendKeys(searchQuery);
        }
        public SearchResultPage ClickSubmitSearchButton()
        {
            FindElement(_searchButtonLocator).Click();
            return new SearchResultPage(_driver);
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
        public void HoverOverServicesLink()
        {
            var servicesElement = FindElement(_servicesLocator);
            var actions = new Actions(_driver);
            actions.MoveToElement(servicesElement).Perform();
        }
        public ServicesOptionPage ClickOnServicesCategoryLink(string linkText)
        {
            FindElement(By.LinkText(linkText)).Click();
            return new ServicesOptionPage(_driver);
        }
    }
}
