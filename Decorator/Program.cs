using NLog;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace Decorator
{
    public interface Component
    {
        string Operation();
    }

    public class Request : Component
    {
        RestRequest request;

        public Request(RestRequest request)
        {
            this.request = request;
        }

        public string Operation()
        {
            return "resourse: " + request.Resource.ToString() + " parameters: " + request.Parameters.ToString() + " method: " + request.Method.ToString();
        }
    }

    public class Response : Component
    {
        RestResponse response;
        public Response(RestResponse response)
        {
            this.response = response;
        }

        public string Operation()
        {
            return response.Content;
        }
    }

    public class BaseDecorator : Component
    {
        private Component component;

        public BaseDecorator(Component component)
        {
            this.component = component;
        }

        public virtual string Operation()
        {
            return component.Operation();
        }
    }

    public class LogDecorator : BaseDecorator
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
   
        public LogDecorator(Component component) : base(component) { }

        public override string Operation()
        {
            Logger.Info( base.Operation());
            return base.Operation();
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            RestClient client = new RestClient("https://reqres.in/");
            RestRequest request = new RestRequest("/api/users");
            RestResponse response = await client.GetAsync(request);

            Component logRequest = new LogDecorator(new Request(request));
            Component logResponse = new LogDecorator(new Response(response));

            Console.WriteLine(logRequest.Operation());
            Console.WriteLine(logResponse.Operation());
            Console.ReadKey();
        }
    }
}
