using OpenQA.Selenium;

namespace BusinessLayer.PageObjects
{
    public class JobSearchPage : BasePage
    {
        readonly By _SearchFieldLocator = By.Name("search");
        readonly By _CountryFieldLocator = By.XPath("//div[@data-testid = 'country-dropdown']//input[contains(@class, 'input')]");
        readonly By _RadioButtonRemoteLocator = By.CssSelector("div[class *= 'sideMenu'] label[for *='checkbox-vacancy_type-Remote']");
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
                        Log.Warn($"Encountered {ex.GetType().Name} while trying to enter programming language into search field. Retrying...");
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
                    Log.Info($"Selecting country: {country}");
                    FindElement(_CountryFieldLocator).Click();
                    ScrollToElement(SetCountryOptionLocator(country));
                    FindElement(SetCountryOptionLocator(country)).Click();
                    Log.Info($"Country selected: {country}");
                    return true;
                }
                catch (Exception ex)
                {
                    if (ex is StaleElementReferenceException || ex is ElementClickInterceptedException)
                    {
                        Log.Warn($"Encountered {ex.GetType().Name} while trying to select country. Retrying...");
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
                    Log.Info("Selecting remote position filter");
                    ScrollToElement(_RadioButtonRemoteLocator);
                    FindElement(_RadioButtonRemoteLocator).Click();
                    Log.Info("Remote position filter selected");
                    return true;
                }
                catch (Exception ex)
                {
                    if (ex is StaleElementReferenceException || ex is ElementClickInterceptedException)
                    {
                        Log.Warn($"Encountered {ex.GetType().Name} while trying to select remote position filter. Retrying...");
                        return false;
                    }
                    else throw;
                }
            });
            }

        public void ClickJobSearchButton()
        {
            _wait.Until(d =>
            {
                try
                {
                    Log.Info("Clicking job search button");
                    ScrollToElement(_SearchButtonLocator);
                    FindElement(_SearchButtonLocator).Click();
                    Log.Info("Job search button clicked");
                    return true;
                }
                catch (Exception ex)
                {
                    if (ex is StaleElementReferenceException || ex is ElementClickInterceptedException)
                    {
                        Log.Warn($"Encountered {ex.GetType().Name} while trying to click job search button. Retrying...");
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
                    Log.Info("Finding relevant job offer");
                    ScrollToElement(_ResultLocator);
                    Log.Info("Clicking to expand job offer details");
                    FindElement(_ResultLocator).Click();
                    for (int i = 0; i < 3; i++)
                    {
                        Log.Info($"Attempt {i + 1}: Scrolling to job offer details");
                        ScrollToElement(SetResultExtendedLocator(programminglanguage));
                    }
                    return revealed.Displayed;
                }
                catch (Exception ex)
                {
                    if (ex is StaleElementReferenceException || ex is ElementClickInterceptedException || ex is NoSuchElementException)
                    {
                        Log.Warn($"Encountered {ex.GetType().Name} while trying to find relevant job offer. Retrying...");
                        return false;
                    }
                    else throw;
                }
            });
            return revealed;
        }
    }
}
