using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ecommerce_POM_Framework.Utilities;
using NUnit.Framework;
using Ecommerce_POM_Framework.POM;
using System.Configuration;
namespace Ecommerce_Project
{
    public class EndToEnd : BaseClass
    {
        // [Test]
        // [TestCase("rahulshettyacademy","learning")]
        // [TestCase("rahulshetty", "learn")]


        [TestCaseSource("TestCaseDataMethod")]
        [Parallelizable(ParallelScope.All)]
        public void Login(string username, string password,string typevalue, string[] ExpectedProduct, string ExpectedFinalMsg)
        {
       
            LoginPage loginpage = new LoginPage(GetDriver(), wait);

            ProductPage ProductObject = loginpage.VaildLogin(username, password, typevalue);

            ProductObject.WaitforProductPageToDisplay();

            ProductSummaryPage SummaryObject = ProductObject.CheckOutProducts();

            FinalPage LastPage = SummaryObject.CheckSummary(ExpectedProduct);

            LastPage.PlaceOrder(ExpectedFinalMsg);
        }
        public static IEnumerable<TestCaseData> TestCaseDataMethod()
        {
            yield return new TestCaseData(GetJsonObject().GetDataReader("username"), GetJsonObject().GetDataReader("password"), 
            GetJsonObject().GetDataReader("typevalue"), GetJsonObject().GetDataReaderForArray("Products"), GetJsonObject().GetDataReader("ExpectedMsg"));

            yield return new TestCaseData(GetJsonObject().GetDataReader("username"), GetJsonObject().GetDataReader("password"), 
            GetJsonObject().GetDataReader("typevalue"), GetJsonObject().GetDataReaderForArray("Products"), GetJsonObject().GetDataReader("ExpectedMsg"));

            yield return new TestCaseData(GetJsonObject().GetDataReader("Wrong_Username"), GetJsonObject().GetDataReader("Wrong_Password"), 
            GetJsonObject().GetDataReader("typevalue"), GetJsonObject().GetDataReaderForArray("Products"), GetJsonObject().GetDataReader("ExpectedMsg"));
        }
        
    }
}
