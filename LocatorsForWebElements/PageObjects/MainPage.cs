using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsForWebElements.PageObjects
{
    internal class MainPage : BasePage
    {
        private readonly string _url;
        readonly By _careersLocator = By.LinkText("Careers");
        readonly By _magnifiericonLocator = By.ClassName("header__icon");
        readonly By _searchFieldLocator = By.Id("new_form_search");
        readonly By _searchButtonLocator = By.XPath("//button[contains(@class,'custom-button')]");
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
    }
}
