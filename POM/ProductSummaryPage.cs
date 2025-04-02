using AngleSharp.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_POM_Framework.POM
{
    public class ProductSummaryPage
    {
        private IWebDriver driver;
        public ProductSummaryPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "h4 a")]
        private IList<IWebElement> Products;

        [FindsBy(How = How.ClassName, Using = "btn-success")]
        private IWebElement Checkoutbtn;

        public IList<IWebElement> GetProducts()
        {
            return Products;
        }
        public IWebElement GetCheckoutbtn()
        {
            return Checkoutbtn;
        }

        public FinalPage CheckSummary(string[] ExpectedProduct)
        {
            
            IList<IWebElement> ActualProductList = GetProducts();

            string[] actualProduct = new string[ActualProductList.Count];

            for (int i=0; i<ActualProductList.Count; i++)
            {
                actualProduct[i] = ActualProductList[i].Text;
                Console.WriteLine(actualProduct[i]);
            }
            Assert.AreEqual(ExpectedProduct, actualProduct);
            

           GetCheckoutbtn().Click();

            return new FinalPage(driver);
        }
    }
}
