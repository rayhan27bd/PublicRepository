namespace Utility
{
    public abstract class BaseTest : IDisposable
    {
        protected IWebDriver? driver;
        protected IWebElement? element;
        protected SelectElement? selectElement;

        [SetUp]
        //[OneTimeSetUp]
        public void Open()
        {
            //driver = new EdgeDriver();
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        //[OneTimeTearDown]
        public void Dispose()
        {
            Thread.Sleep(2000); driver?.Quit();
        }
    }
}
