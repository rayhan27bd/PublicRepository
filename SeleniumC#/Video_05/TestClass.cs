namespace Video_05
{
    public class TestClass : BaseTest
    {
        [Test, Category("Smoke Testing")]
        public void TestMethod_01()
        {
            TestMethod();
            element = driver?.FindElement(By.Id("UserName"));      // or
            element?.SendKeys("tauhid@onnorokom.com");
            Thread.Sleep(2000);
        }

        [Test, Category("Regression Testing")]
        public void TestMethod_02()
        {
            TestMethod();
            element = driver?.FindElement(By.Name("UserName"));    // or
            element?.SendKeys("polash@onnorokom.com");
            Thread.Sleep(2000);
        }

        [Test, Category("Smoke Testing")]
        public void TestMethod_03()
        {
            TestMethod();
            element = driver?.FindElement(By.XPath("//*[@id=\"UserName\"]"));
            element?.SendKeys("shamim@onnorokom.com");
            Thread.Sleep(2000);
        }

        private void TestMethod()
        {
            driver.Url = "https://ums.osl.team/Account/Login";
        }
    }
}