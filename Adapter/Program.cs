using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using RestSharp;

namespace Adapter
{
    public class DataWorker
    {
        public static string ConvertJSONStringToCamelCase(string jsonString)
        {
            Dictionary<string, string> dictionary = ConvertJsonStringToDictionary(jsonString);
            dictionary = ConvertKeyInDictionaryToCamelCase(dictionary);
            return ConvertObjectToJSONString(dictionary);
        }

        public static Dictionary<string, string> ConvertJsonStringToDictionary(string jsonString)
        {
            return jsonString.Split(",")
                   .Select(part => part.Split(":"))
                   .Where(part => part.Length == 2)
                   .ToDictionary(part => part[0], part => part[1]);
        }

        public static Dictionary<string, string> ConvertKeyInDictionaryToCamelCase(Dictionary<string, string> dictionary)
        {
            Dictionary<string, string> dictionaryWithCamelCaseKey = new Dictionary<string, string>();
            foreach (string str in dictionary.Keys)
            {
                var words = str.Split(new[] { "_", " ", "\'" }, StringSplitOptions.RemoveEmptyEntries);
                words = words
                    .Select(word => char.ToUpper(word[0]) + word.Substring(1))
                    .ToArray();
                string key = string.Join(string.Empty, words);
                dictionaryWithCamelCaseKey.Add(key, dictionary[str]);
            }

            return dictionaryWithCamelCaseKey;
        }

        public static string ConvertObjectToJSONString(object someObject)
        {
            return JsonSerializer.Serialize(someObject);
        }

        public virtual void Display()
        {
            Console.WriteLine("This is DataFormat class!");
        }

    }

    public class Country
    {
        string countryName;
        public Country(string countryName)
        {
            this.countryName = countryName;
        }

        public async Task<RestResponse> GetCountry()
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"http://127.0.0.1:5000/country?name={countryName}", Method.Get);
            return await client.ExecuteAsync(request);
        }
    }

    public class CountryAdapter : DataWorker
    {
        private Country country;
        public CountryAdapter(Country country)
        {
            this.country = country;
        }

        public override void Display()
        {
            string countryString = country.GetCountry().Result.Content;
            Console.WriteLine(ConvertJSONStringToCamelCase(countryString));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DataWorker countryData = new CountryAdapter(new Country("Ukraine"));
            countryData.Display();
        }
    }
}
