namespace SeleniumCSharp
{
    public class BaseTest
    {
        public IWebDriver? driver;
        public IWebElement? element;

        //[SetUp]
        [OneTimeSetUp]
        public void Open()
        {
            //driver = new EdgeDriver();
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            driver.Url = "https://ums.osl.team/Account/Login";
        }

        //[TearDown]
        [OneTimeTearDown]
        public void Close()
        {
            Thread.Sleep(2000);
            driver?.Quit();
        }
    }
}
