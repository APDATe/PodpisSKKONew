using System.Text.Json;
using System.Text.Json.Nodes;

namespace testWebSKKO.Services
{
    public static class JsonHelper
    {
        public static string Canonicalize(object obj)
        {
            var node = JsonNode.Parse(JsonSerializer.Serialize(obj))!;
            var sortedNode = SortPropertiesRecursively(node);
            var options = new JsonSerializerOptions
            {
                WriteIndented = false,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            return sortedNode.ToJsonString(options);
        }

        private static JsonNode SortPropertiesRecursively(JsonNode node)
        {
            if (node is JsonObject obj)
            {
                var sorted = new JsonObject();
                foreach (var prop in obj.OrderBy(p => p.Key))
                {
                    if (prop.Value is not null)
                    {
                        var cloned = prop.Value.DeepClone();
                        sorted.Add(prop.Key, SortPropertiesRecursively(cloned));
                    }
                    else
                    {
                        // Просто добавляем null как есть
                        sorted.Add(prop.Key, null);
                    }
                }
                return sorted;
            }
            else if (node is JsonArray arr)
            {
                var sortedArray = new JsonArray();
                foreach (var item in arr)
                {
                    var clonedItem = item?.DeepClone();
                    sortedArray.Add(clonedItem is not null ? SortPropertiesRecursively(clonedItem) : null);
                }
                return sortedArray;
            }

            return node;
        }
    }
}
