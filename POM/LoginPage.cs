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
    public class LoginPage
    {
        private IWebDriver driver;
        public WebDriverWait wait;
        public LoginPage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
            PageFactory.InitElements(driver, this);
        }

        //PageFactory Design Pattern 

        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement username;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement password;

        [FindsBy(How = How.XPath, Using = "//form[@id='login-form']/div[4]/div/label[2]/span[2]")]
        private IWebElement RadioBtn;

        [FindsBy(How = How.Id, Using = "okayBtn")]
        private IWebElement AlertPage;

        [FindsBy(How = How.XPath, Using = "//select[@class='form-control']")]
        private IWebElement Dropdown;

        [FindsBy(How = How.XPath, Using = "//form[@id='login-form']/div[6]/label[1]/span[1]")]
        private IWebElement Checkbox;

        [FindsBy(How = How.Id, Using = "signInBtn")]
        private IWebElement SignInBtn;

        //Login Method
        public ProductPage VaildLogin(string userid, string pass, string typevalue) {

            username.SendKeys(userid);
            password.SendKeys(pass);
            RadioBtn.Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("okayBtn")));
            AlertPage.Click();
            IWebElement element = Dropdown;
            SelectElement s = new SelectElement(element);
            s.SelectByValue(typevalue);
            Checkbox.Click();
            SignInBtn.Click();
            return new ProductPage(driver);
        }
    }
}
