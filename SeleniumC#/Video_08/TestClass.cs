
namespace Video_08
{
    [TestFixture]
    public class TestClass : BaseTest
    {
        [Test]
        public void TestMethod1()
        {
            TestMethod();
        }

        [Test]
        public void TestMethod2()
        {
            TestMethod();
        }
        
        private void TestMethod()
        {
            _driver.Url = "https://ums.osl.team/Account/Login";

            _element = _driver.FindElement(By.Id("UserName"));
            _element?.SendKeys("tauhid@onnorokom.com");
            Thread.Sleep(1000);
        }
    }
}