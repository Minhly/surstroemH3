using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ProductDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("product_title")]
        public string Title { get; set; }
        [JsonProperty("short_description")]
        public string ShortDescription { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("price")]
        public double Height { get; set; }
        [JsonProperty("warranty_period_id")]
        public int WarrantyPeriodId { get; set; }
        [JsonProperty("color_id")]
        public int ColorId { get; set; }
        [JsonProperty("brand_id")]
        public int BrandId { get; set; }
    }
}
