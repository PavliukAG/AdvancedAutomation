using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Builder
{
    public interface IBuilder
    {
        Object AddDescription(string description);
        Object AddInitNotation(string next);
        Object AddActionNotation(string name, string next, string action);
        Object AddWaitNotation(string name, string next, string wait);
        Object AddEndNotation();
    }

    public class Director
    {
        private IBuilder builder;

        public Director(Object builder)
        {
            this.builder = builder;
        }

        public Object BuildActionObject(string action)
        {
            return builder.AddDescription("timeout_wf")
                .AddInitNotation("message_node")
                .AddActionNotation("message_node", "wait_for_interaction", action)
                .AddWaitNotation("wait_for_interaction", "end", "timer.wait(100)")
                .AddEndNotation();
        }
    }

    public class Object : IBuilder
    {
        dynamic jsonObject = new Dictionary<object, object>
        {
            {  "notation", new List<object>() }            
        };

        public Object AddActionNotation(string name, string next, string action)
        {
            jsonObject["notation"].Add(new Dictionary<object, object>
                {
                    {
                        "name", name
                    },
                    {
                        "next", next
                    },
                    {
                        "type", "action"
                    },
                    {
                        "action", action
                    }
                }
            );
            return this;
        }

        public Object AddDescription(string description)
        {
            jsonObject.Add("description", description);
            return this;
        }

        public Object AddWaitNotation(string name, string next, string wait)
        {
            jsonObject["notation"].Add(new Dictionary<object, object>
                {
                    { "name", name },
                    { "next", next },
                    {"type", "wait" },
                    {"wait", wait }
                } 
            );          
            return this;
        }

        public Object AddInitNotation(string next)
        {
            jsonObject["notation"].Add(new Dictionary<object, object>
                {
                    { "name", "init"},
                    {"next", next },
                    {"type", "init" }
                }
            );
            return this;
        }

        public Object AddEndNotation()
        {
            jsonObject["notation"].Add(new Dictionary<object, object>
                {
                    {"name", "end" },
                    {"type", "end" }
                }
            );
            return this;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new Object();
            var director = new Director(builder);
            var action = director.BuildActionObject("bus.sendInteraction('hi')");
            Console.WriteLine(action.ToString());
        }
    }
}
