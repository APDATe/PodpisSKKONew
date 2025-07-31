using System.Text.Json;
using testWebSKKO.Models;
using testWebSKKO.Models;

namespace testWebSKKO.Services
{
    public static class DocumentGenerator
    {
        public static Document Create()
        {
            var doc = new Document
            {
                address = "г. Гомель (400), ул. УЛ. ГОМЕЛЬСКАЯ, д. 1",
                currency = "BYN",
                gni_location = "400",
                issued_at = "2025-07-31T10:10:00",
                message_number = 2,
                trader_unp = 491332034,
                trading_object_name = "ООО РОга и Копытца",
                positions = new List<Position>()
            };

            long baseEan = 4816149301000;

            for (int i = 0; i < 100; i++)
            {
                string ean = (baseEan + i).ToString();
                if (ean.Length == 13) ean = "0" + ean;

                var position = new Position
                {
                    amount = 10 + i,
                    discount = 0,
                    ean = ean,
                    marking_code = $"01{ean}212Code{i}�910005�92SomeUnique{i}",
                    marking_type = 17,
                    name = $"Товар №{i + 1}",
                    product_count = 1,
                    surcharge = 0,
                    ukz_code = null
                };

                doc.positions.Add(position);
            }

            return doc;
        }
    }
}
