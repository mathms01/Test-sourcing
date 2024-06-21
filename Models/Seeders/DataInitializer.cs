using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Test_sourcing.Models.Seeders
{
    public static class DataInitializer
    {
        public static async Task<XPathConfiguration?> LoadConfig()
        {
            JsonSerializerOptions options = new();
            options.PropertyNameCaseInsensitive = true;
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            string file = "WebsiteNodeConfigs.json";
            string filename = Path.Combine(".", "Models", "Seeders", file);
            string referenceJson =
                await File.ReadAllTextAsync(filename);
            return JsonSerializer.Deserialize<XPathConfiguration>(referenceJson, options);
        }
    }
}
