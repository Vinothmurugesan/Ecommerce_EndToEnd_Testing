using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Ecommerce_POM_Framework.Utilities
{
    public class JsonReader
    {
        public string GetDataReader(string TokenName)
        {
            string FileText = File.ReadAllText("C:\\Source\\Project Ecommerce\\Ecommerce POM Framework\\Utilities\\DataFile.json");
            var JsonObject = JToken.Parse(FileText);
            return JsonObject.SelectToken(TokenName).Value<string>();
        }
        public string[] GetDataReaderForArray(string TokenName)
        {
            string FileText = File.ReadAllText("C:\\Source\\Project Ecommerce\\Ecommerce POM Framework\\Utilities\\DataFile.json");
            var JsonObject = JToken.Parse(FileText);
            List<string> products =  JsonObject.SelectTokens(TokenName).Values<string>().ToList();
            return products.ToArray();
        }
    }
}
