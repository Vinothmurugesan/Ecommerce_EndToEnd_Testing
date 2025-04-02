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
    public class ProductPage
    {
        private IWebDriver driver;
        public IWebElement ProductAdd;
        By VisibilityOfAddbtn = By.XPath("//button[text()='Add ']");
        public ProductPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//button[text()='Add ']")]
        private IList<IWebElement> ProductList;

        [FindsBy(How = How.PartialLinkText, Using = "Checkout ( 2 )")]
        private IWebElement CheckOutbtn;

        public IList<IWebElement> GetProductList()
        {
            return ProductList;
        }
        public By GetAddBtn()
        {
            return VisibilityOfAddbtn;
        }
        public IWebElement GetCheckOutBtn()
        {
            return CheckOutbtn;
        }
        public void WaitforProductPageToDisplay()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            ProductAdd = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[text()='Add ']")));
            
        }

        public ProductSummaryPage CheckOutProducts()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", ProductAdd);
            IList<IWebElement> ProductList = GetProductList();
            ProductList[0].Click();
            ProductList[3].Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement CheckOutBtn = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout ( 2 )")));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", CheckOutBtn);
            GetCheckOutBtn().Click();
            return new ProductSummaryPage(driver);
        }

    }
   
}
