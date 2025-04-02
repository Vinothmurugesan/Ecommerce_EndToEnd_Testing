using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V131.Browser;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using AventStack.ExtentReports;
using System.Security.Cryptography.X509Certificates;




namespace Ecommerce_POM_Framework.Utilities
{
    public class BaseClass
    {
        protected WebDriverWait wait;
        string Browser;
        ExtentTest test;
        ExtentReports extent;

        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        [OneTimeSetUp]
        public void Setup()
        {
            string CurrentPath = Directory.GetCurrentDirectory();
            string ParentPathName = Directory.GetParent(CurrentPath).Parent.Parent.FullName;
            var sparkReporter = new ExtentHtmlReporter(ParentPathName + "//index.html");
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AttachReporter(sparkReporter);
            extent.AddSystemInfo("Host Name", "Local Host");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Username", "Vinoth Murugesan");
        }
        [SetUp]
        public void SetUp()
        {

            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

            Browser = TestContext.Parameters["Browser"];

            if (Browser == null)
            {
                Browser = ConfigurationManager.AppSettings["Browser"];

            }
            InitiateBrowser(Browser);

            driver.Value.Manage().Window.Maximize();
            driver.Value.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise");
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(5));
        }

        public static JsonReader GetJsonObject() => new JsonReader();

        public IWebDriver GetDriver() => driver.Value;

        public void InitiateBrowser(string browser)
        {
            switch (browser.ToLower())
            {
                case "chrome":
                    driver.Value = new ChromeDriver();
                    break;
                case "edge":
                    driver.Value = new EdgeDriver();
                    break;
                case "firefox":
                    driver.Value = new FirefoxDriver();
                    break;
                default:
                    throw new ArgumentException("Invalid browser type");
            }
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;

            DateTime dateTime = DateTime.Now;
            string ScreenShotName = "ScreenShot" + dateTime.ToString("hh_mm_ss") + ".png";
            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                test.Fail("Test Failed", CaptureScreenShot(driver.Value, ScreenShotName));
                test.Log(Status.Fail, "Test Failed with LogTrace" + stackTrace);
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {

            }

            driver.Value.Quit();
        }
        [OneTimeTearDown]
        public void OnetimeTearDown()
        {
            extent.Flush();
        }
        public MediaEntityModelProvider CaptureScreenShot(IWebDriver driver, string screenshotName)
        {
            ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;
            string screenshotBase64 = screenshotDriver.GetScreenshot().AsBase64EncodedString;

            // Correct usage of CreateScreenCaptureFromBase64String
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshotBase64).Build();
        }

    }
}
