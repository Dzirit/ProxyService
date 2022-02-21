using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NasaEpicApiClient.Models
{
    public class ImageModel
    {
        [JsonProperty(PropertyName = "identifier")]
        public string Id { get; set; }

        public string Caption { get; set; }

        public string Image { get; set; }

        public string Version { get; set; }

        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "coords")]
        public Coordinate Coordinate { get; set; }
    }

    public class RawImage
    {
        public byte[] ImageData { get; set; }
    }
}
