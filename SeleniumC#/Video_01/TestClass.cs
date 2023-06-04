namespace Video_01
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

            _element = _driver.FindElement(By.Id("UserName"));      // or
            _element = _driver.FindElement(By.Name("UserName"));    // or
            _element = _driver.FindElement(By.XPath("//*[@id=\"UserName\"]"));

            _element.SendKeys("tauhid@onnorokom.com");
            Thread.Sleep(2000); _driver.Quit();
        }
    }
}