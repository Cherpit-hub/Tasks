using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LocatorsForWebElements.BusinessLayer.PageObjects
{
    public class MainPage : BasePage
    {
        private readonly string _url;
        readonly By _careersLocator = By.LinkText("Careers");
        readonly By _magnifiericonLocator = By.ClassName("header__icon");
        readonly By _searchFieldLocator = By.Id("new_form_search");
        readonly By _searchButtonLocator = By.XPath("//button[contains(@class,'custom-button')]");
        //readonly By _codeOfConductLocator = By.LinkText("Code of Ethical Conduct (PDF)");//
        readonly By _codeOfConductLocator = By.XPath("//div[contains(@class,'footer')]//a[contains(text(),'Ethical')]");
        readonly By _insightLocator = By.PartialLinkText("Insight");
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
        public void ClickSearchButton()
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
    }
}
