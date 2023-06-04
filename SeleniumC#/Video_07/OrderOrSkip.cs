namespace Video_07
{
    [TestFixture]
    public class OrderOrSkip
    {
        private IWebDriver _driver;
        private IWebElement _element;

        [Test, Order(3), Category("OrderOrSkipAttribute")]
        public void TestMethod_01()
        {
            _driver = new ChromeDriver();
            BrowserUtility(_driver);

            _element = _driver.FindElement(By.Id("UserName"));
            _element?.SendKeys("tauhid@onnorokom.com");
            Thread.Sleep(2000); _driver.Quit();
        }
        
        [Test, Order(1), Category("OrderOrSkipAttribute")]
        public void TestMethod_02()
        {
            _driver = new EdgeDriver();
            BrowserUtility(_driver);

            _element = _driver.FindElement(By.Id("UserName"));
            _element?.SendKeys("tauhid@onnorokom.com");
            Thread.Sleep(2000); _driver.Quit();
        }
        
        [Test, Order(2), Category("OrderOrSkipAttribute")]
        public void TestMethod_03()
        {
            Assert.Ignore("Skip");
            _driver = new ChromeDriver();
            BrowserUtility(_driver);
            _element = _driver.FindElement(By.Id("UserName"));
            _element?.SendKeys("tauhid@onnorokom.com");
            Thread.Sleep(2000); _driver.Quit();
        }

        private static void BrowserUtility(IWebDriver driver)
        {
            driver.Manage().Window.Maximize();
            driver.Url = "https://ums.osl.team/Account/Login";
        }
    }
}
