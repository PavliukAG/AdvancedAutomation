using OpenQA.Selenium;

namespace TemplateMethod
{
    public abstract class MainBehaivor
    {
        public Page page;
        protected MainBehaivor(Page page)
        {
            this.page = page;
        }

        public void TemplateMethod(string data)
        {
            Navigate();
            EnterData(data);
            ClickButton();
        }
        public void Navigate()
        {
            page.driver.Navigate().GoToUrl("http://127.0.0.1:5000");
        }
        public abstract void EnterData(string data);
        public abstract void ClickButton();
    }

    public class GetCapitalData : MainBehaivor
    {
        public GetCapitalData(Page page) : base(page)
        {
            this.page = page;
        }

        public override void EnterData(string data)
        {
            page.capitalField.SendKeys(data);
        }

        public override void ClickButton()
        {
            page.capitalButton.Click();
        }
    }

    public class GetCountryData : MainBehaivor
    {
        public GetCountryData(Page page) : base(page)
        {
            this.page = page;
        }

        public override void EnterData(string data)
        {
            page.countryField.SendKeys(data);
        }

        public override void ClickButton()
        {
            page.countryButton.Click();
        }
    }

    public class Page
    {
        public IWebDriver driver;
        public IWebElement capitalField => driver.FindElement(By.XPath("//input[@name='capital']"));
        public IWebElement countryField => driver.FindElement(By.XPath("//input[@name='country']"));
        public IWebElement capitalButton => driver.FindElement(By.XPath("//button[@name='capital_button']"));
        public IWebElement countryButton => driver.FindElement(By.XPath("//button[@name='country_button']"));
    }

    class Program
    {
        static void Main(string[] args)
        {
            Page page = new Page();
            GetCapitalData getCapitalData = new GetCapitalData(page);
            getCapitalData.TemplateMethod("Kyiv");

            GetCountryData getCountryData = new GetCountryData(page);
            getCountryData.TemplateMethod("Latvia");
        }
    }
}
