namespace Utility
{
    public static class TestHelper
    {
        public static TimeSpan ImplicitWaitTime(IWebDriver driver)
        {
            return driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }
    }
}
