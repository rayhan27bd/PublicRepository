namespace SeleniumCSharp
{
    [TestFixture]
    public class TestClass //: BaseTest
    {
        private IWebDriver? _driver;
        private IWebElement? _element;

        [Test]
        public void TestMethod_01()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();

            _driver.Url = "https://ums.osl.team/Account/Login";

            _element = _driver.FindElement(By.Id("UserName"));
            _element.SendKeys("tauhid@onnorokom.com");
            _driver.Quit();
        }
    }
}