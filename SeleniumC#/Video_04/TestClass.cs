namespace Video_04
{
    public class TestClass : IDisposable
    {
        private IWebDriver driver;
        private IWebElement? element;
        private SelectElement? selectElement;

        [SetUp]
        public void SetUp()
        {
            //driver = new EdgeDriver();
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            driver.Url = "https://www.facebook.com/";
        }

        [Test, Category("Smoke Testing")]
        public void TestMethod_01()
        {
            TestHelper.ImplicitWaitTime(driver);
            driver.FindElement(By.XPath(IXPathConstant.CreateNewAccount)).Click();

            TestHelper.ImplicitWaitTime(driver);
            element = driver?.FindElement(By.Id("month"));

            selectElement = new SelectElement(element);
            Thread.Sleep(2000);
            selectElement.SelectByIndex(11);
            Thread.Sleep(2000);
            selectElement.SelectByValue("10");
            Thread.Sleep(2000);
            selectElement.SelectByText("Apr");
        }

        [Test, Category("Smoke Testing")]
        public void TestMethod_03()
        {
            TestHelper.ImplicitWaitTime(driver);
            driver.FindElement(By.XPath(IXPathConstant.CreateNewAccount)).Click();

            TestHelper.ImplicitWaitTime(driver);
            element = driver?.FindElement(By.Name("birthday_month"));

            selectElement = new SelectElement(element);
            Thread.Sleep(2000);
            selectElement.SelectByValue("10");
            Thread.Sleep(2000);
            selectElement.SelectByText("Apr");
        }

        [TearDown]
        public void Dispose()
        {
            Thread.Sleep(2000); driver?.Quit();
        }
    }
}