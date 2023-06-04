//Parallel Testing
using Video_06.Utilities;

namespace Video_06
{
    [TestFixture]
    public class ParallelTesting
    {
        private readonly IWebDriver? _driver;
        private IWebElement? element;

        [Test, Category("UA Testing"), Category("Module_01")]
        public void TestMethod_01()
        {
            CommonTestMethod();
        }

        [Test, Category("UA Testing"), Category("Module_01")]
        public void TestMethod_02()
        {
            CommonTestMethod();
        }

        [Test, Category("UA Testing"), Category("Module_01")]
        public void TestMethod_03()
        {
            CommonTestMethod();
        }

        [Test, Category("UA Testing"), Category("Module_01")]
        public void TestMethod_04()
        {
            CommonTestMethod();
        }

        private void CommonTestMethod()
        {
            var driver = new Browser().Init(_driver);
            element = driver?.FindElement(By.Id("UserName"));   // or
            element?.SendKeys("tauhid@onnorokom.com");
            Thread.Sleep(2000); driver?.Quit();
        }
    }
}