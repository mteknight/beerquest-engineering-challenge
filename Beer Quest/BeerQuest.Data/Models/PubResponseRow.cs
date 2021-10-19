using System;

using Newtonsoft.Json;

namespace BeerQuest.Data.Models
{
    public record PubResponseRow
    {
        public string? Name { get; set; }

        public string? Category { get; set; }

        public string? Url { get; set; }

        public DateTime Date { get; set; }

        public string? Excerpt { get; set; }

        public string? Thumbnail { get; set; }

        [JsonProperty("lat")]
        public float Latitude { get; set; }

        [JsonProperty("lng")]
        public float Longitude { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Twitter { get; set; }

        [JsonProperty("stars_beer")]
        public double StarsBeer { get; set; }

        [JsonProperty("stars_atmosphere")]
        public double StarsAtmosphere { get; set; }

        [JsonProperty("stars_amenities")]
        public double StarsAmenities { get; set; }

        [JsonProperty("stars_value")]
        public double StarsValue { get; set; }

        public string? Tags { get; set; }
    }
}
