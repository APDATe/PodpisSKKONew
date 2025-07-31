namespace testWebSKKO.Models
{
    public class Position
    {
        public decimal amount { get; set; }
        public decimal discount { get; set; }
        public string ean { get; set; }
        public string marking_code { get; set; }
        public int marking_type { get; set; }
        public string name { get; set; }
        public int product_count { get; set; }
        public decimal surcharge { get; set; }
        public string ukz_code { get; set; }
    }

    public class Document
    {
        public string address { get; set; }
        public string currency { get; set; }
        public string gni_location { get; set; }
        public string issued_at { get; set; }
        public int message_number { get; set; }
        public List<Position> positions { get; set; }
        public long trader_unp { get; set; }
        public string trading_object_name { get; set; }
    }

}
