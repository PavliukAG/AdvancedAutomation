using System;
using System.Net;

namespace Strategy
{
    public class Context
    {
        IStrategy strategy;
        public Context()
        { }
        public Context(IStrategy strategy)
        {
            this.strategy = strategy;
        }
        public void SetStrategy(IStrategy strategy)
        {
            this.strategy = strategy;
        }
        public void PrintCapitalInformation(object data)
        {
            Console.WriteLine(strategy.GetCapitalInformation(data));
        }
    }

    public interface IStrategy
    {
        object GetCapitalInformation(object data);
    }

    public class WSStrategy : IStrategy
    {
        public object GetCapitalInformation(object data)
        {
            //What are going on???????????
            return data;
        }
    }
    public class APIStrategy : IStrategy
    {
        public object GetCapitalInformation(object data)
        {
            WebRequest request = WebRequest.Create($"https://restcountries.com/v3.1/capital/{data}");
            WebResponse webResponse = request.GetResponse();
            return webResponse;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var context = new Context();
            context.SetStrategy(new APIStrategy());
            context.PrintCapitalInformation("Riga");

            context.SetStrategy(new WSStrategy());
            context.PrintCapitalInformation("Kyiv");
        }
    }
}
