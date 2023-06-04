namespace Video_02
{
    [TestFixture]
    public class TestClass : BaseTest
    {
        [Test]
        public void TestMethod_01()
        {
            element = driver.FindElement(By.Id("UserName"));      // or
            element.SendKeys("tauhid@onnorokom.com");
            Thread.Sleep(2000); //_driver.Quit();

            element.Clear();
            element = driver.FindElement(By.Name("UserName"));    // or
            element.SendKeys("polash@onnorokom.com");
            Thread.Sleep(2000); //_driver.Quit();

            element.Clear();
            element = driver.FindElement(By.XPath("//*[@id=\"UserName\"]"));
            element.SendKeys("tauhid@onnorokom.com");
            Thread.Sleep(2000); //_driver.Quit();
        }
        
        [Test]
        public void TestMethod_02()
        {
            element = driver.FindElement(By.Id("UserName"));      // or
            element = driver.FindElement(By.Name("UserName"));    // or
            element = driver.FindElement(By.XPath("//*[@id=\"UserName\"]"));

            element.SendKeys("tauhid@onnorokom.com");
            Thread.Sleep(2000); //_driver.Quit();
        }
    }
}