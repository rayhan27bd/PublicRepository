namespace Video_06.Utilities
{
    public class Browser
    {
        public IWebDriver Init(IWebDriver driver)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            //driver.Url = "https://www.facebook.com";
            driver.Url = "https://ums.osl.team/Account/Login";

            return driver;
        }
    }
}
