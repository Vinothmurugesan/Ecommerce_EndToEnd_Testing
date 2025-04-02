using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_POM_Framework.POM
{
    public class FinalPage
    {
        private IWebDriver driver;
        public FinalPage(IWebDriver driver) {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        By listOfCountry = By.XPath("//div[@class='suggestions']/ul/li/a");

        [FindsBy(How = How.Id, Using = "country")]
        private IWebElement Country;

        [FindsBy(How = How.PartialLinkText, Using = "term & Conditions")]
        private IWebElement Terms;

        [FindsBy(How = How.CssSelector, Using = "button[class = 'btn btn-info']")]
        private IWebElement Closebtn;

        [FindsBy(How = How.XPath, Using = "//input[@value='Purchase']")]
        private IWebElement PurchaseBtn;

        [FindsBy(How = How.CssSelector, Using = "div[class='alert alert-success alert-dismissible']")]
        private IWebElement Successmsg;

        public IWebElement GetCountry()
        {
            return Country;
        }
        public By GetCountryList()
        {
            return listOfCountry;
        }

        public IWebElement GetTerms()
        {
            return Terms;
        }

        public IWebElement GetCloseBtn()
        {
            return Closebtn;
        }
        public IWebElement GetPurchaseBtn()
        {
            return PurchaseBtn;
        }

        public IWebElement GetSuccessmsg()
        {
            return Successmsg;
        }
        

        public void PlaceOrder(string ExpectedMsg)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            GetCountry().SendKeys("uk");

            IList<IWebElement> list = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(GetCountryList()));

            foreach (IWebElement element in list)
            {
                if (element.Text.Equals("Ukraine"))
                {
                    element.Click();
                }

            }
            GetTerms().Click();
            GetCloseBtn().Click();
            GetPurchaseBtn().Click();

            string OrderPlacedMsg = GetSuccessmsg().Text;
            bool FinalOutput = OrderPlacedMsg.Contains(ExpectedMsg);

            if (FinalOutput)
            {
                Console.WriteLine("Workflow Completed Successfully");
            }
            else
            {
                Console.WriteLine("Workflow Not Completed Successfully");
            }
        }
    }
}
