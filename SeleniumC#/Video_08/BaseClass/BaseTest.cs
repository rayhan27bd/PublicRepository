
namespace Video_08.BaseClass
{
    public abstract class BaseTest : IDisposable
    {
        protected IWebDriver _driver;
        protected IWebElement? _element;

        [OneTimeSetUp]
        public void Open()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void Dispose() => _driver.Quit();
    }
}
