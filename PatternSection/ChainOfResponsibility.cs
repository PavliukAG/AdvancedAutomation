using OpenQA.Selenium;

namespace PatternSection
{
    public class Page
    {
        public IWebDriver driver;
        public IWebElement askCapitalField => driver.FindElement(By.XPath("//input[@id='askCapital']"));
        public IWebElement askCapitalButton => driver.FindElement(By.XPath("//button[@name='Ask capital']"));
    }

    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        object Handle(IWebElement webElement);
    }

    abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;
            return handler;
        }

        public virtual object Handle(IWebElement webElement)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(webElement);
            }
            else
            {
                return null;
            }
        }
    }

    class CheckVisibility : AbstractHandler
    {
        public override object Handle(IWebElement webElement)
        {
            if (webElement.Displayed)
            {
                return webElement;
            }
            else
            {
                return base.Handle(webElement);
            }
        }
    }

    class ClickButton : AbstractHandler
    {
        public override object Handle(IWebElement webElement)
        {
            if (webElement.Selected)
            {
                webElement.Click();
                return webElement;
            }
            else
            {
                return base.Handle(webElement);
            }
        }
    }

    class JSClick : AbstractHandler
    {
        public override object Handle(IWebElement webElement)
        {
            Page page = new Page();
            if (webElement.Selected) 
            {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)page.driver;
                executor.ExecuteScript("arguments[0].click();", webElement);
                return webElement;
            }
            else 
            {
                return base.Handle(webElement); 
            }
        }        
    }

    class Program
    {
        static void Main(string[] args)
        {
            string text = "some text.";
            Page page = new Page();
            page.driver.Navigate().GoToUrl("http://127.0.0.1:5000");
            page.askCapitalField.SendKeys(text);

            CheckVisibility checkVisibility = new CheckVisibility();
            ClickButton clickButton = new ClickButton();
            JSClick jSClick = new JSClick();

            checkVisibility.SetNext(clickButton).SetNext(jSClick);
        }
    }
}