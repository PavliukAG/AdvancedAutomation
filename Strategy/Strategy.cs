using RestSharp;
using System;
using System.Threading.Tasks;

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
            //some ws code implementation
            return data;
        }
    }
    public class APIStrategy : IStrategy
    {
        public async Task<RestResponse> GetCapital(string data)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($@"https://restcountries.com/v3.1/capital/{data}", Method.Get);
            return await client.ExecuteAsync(request);
        }

        public object GetCapitalInformation(object data)
        {
            return GetCapital(data.ToString()).Result.Content;
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
