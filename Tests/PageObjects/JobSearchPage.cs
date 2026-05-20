using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.PageObjects
{
    internal class JobSearchPage : BasePage
    {
        readonly By _SearchFieldLocator = By.Name("search");
        readonly By _CountryFieldLocator = By.XPath("//div[@data-testid = 'country-dropdown']//input[contains(@class, 'input')]");
        readonly By _CountryFieldCleanerLocator = By.XPath("//div[contains(@class,'clear-indicator')]");
        readonly By _RadioButtonRemoteLocator = By.CssSelector("label[for *='checkbox-vacancy_type-Remote']");
        readonly By _SearchButtonLocator = By.XPath("//button[@type='submit' and contains(@name,'submit_search_box')]");
        readonly By _ResultLocator = By.XPath("//div[contains(@data-testid,'accordion-section-container')]");

        public JobSearchPage(IWebDriver driver) : base(driver)
        {
        }
        public static By SetCountryOptionLocator(string country)
        {
            return By.XPath($"//div[@role='listbox']//child::*[contains(text(), '{country}')]");
        }

        public static By SetResultExtendedLocator(string programminglanguage)
        {
            return By.XPath($"//div[contains(@data-testid, 'categories-container')]//descendant::div[contains(text(),'{programminglanguage}')]");
        }

        public void EnterProgrammingLanguageIntoSearchField(string programminglanguage)
        {
            _wait.Until(d =>
            {
                try
                {
                    FindElement(_SearchFieldLocator).SendKeys(programminglanguage + Keys.Enter);
                    return true;
                }
                catch (Exception ex)
                {
                    if (ex is StaleElementReferenceException || ex is ElementClickInterceptedException || ex is ElementNotInteractableException)
                    {
                        return false;
                    }
                    else throw;
                }
            });
        }
        public void ClearCountryField()
        {
            _wait.Until(d =>
            {
                try
                {
                    FindElement(_CountryFieldCleanerLocator).Click();
                    return true;
                }
                catch (Exception ex)
                {
                    if (ex is StaleElementReferenceException || ex is ElementClickInterceptedException || ex is ElementNotInteractableException)
                    {
                        return false;
                    }
                    else throw;
                }
            });
        }
        public void SelectCountry(string country)
        {
            _wait.Until(d =>
            {
                try
                {
                    FindElement(_CountryFieldLocator).Click();
                    ScrollToElement(SetCountryOptionLocator(country));
                    FindElement(SetCountryOptionLocator(country)).Click();
                    return true;
                }
                catch (Exception ex)
                {
                    if (ex is StaleElementReferenceException || ex is ElementClickInterceptedException)
                    {
                        return false;
                    }
                    else throw;
                }
            });
        }

        public void ClickRemotePositionRadioButton()
        {
            _wait.Until(d =>
            {
                try
                {
                    ScrollToElement(_RadioButtonRemoteLocator);
                    FindElement(_RadioButtonRemoteLocator).Click();
                    return true;
                }
                catch (Exception ex)
                {
                    if (ex is StaleElementReferenceException || ex is ElementClickInterceptedException)
                    {
                        return false;
                    }
                    else throw;
                }
            });
            }

        public void ClickSearchButton()
        {
            _wait.Until(d =>
            {
                try
                {
                    ScrollToElement(_SearchButtonLocator);
                    FindElement(_SearchButtonLocator).Click();
                    return true;
                }
                catch (Exception ex)
                {
                    if (ex is StaleElementReferenceException || ex is ElementClickInterceptedException)
                    {
                        return false;
                    }
                    else throw;
                }
            });
        }

        public IWebElement FindRelevantJobOffer(string programminglanguage)
        {
            IWebElement revealed = FindElement(SetResultExtendedLocator(programminglanguage));
            _wait.Until(d =>
            {
                try
                {
                    ScrollToElement(_ResultLocator);
                    FindElement(_ResultLocator).Click();
                    for (int i = 0; i < 3; i++)
                    {
                        ScrollToElement(SetResultExtendedLocator(programminglanguage));
                    }
                    return revealed.Displayed;
                }
                catch (Exception ex)
                {
                    if (ex is StaleElementReferenceException || ex is ElementClickInterceptedException)
                    {
                        return false;
                    }
                    else throw;
                }
            });
            return revealed;
        }
    }
}
