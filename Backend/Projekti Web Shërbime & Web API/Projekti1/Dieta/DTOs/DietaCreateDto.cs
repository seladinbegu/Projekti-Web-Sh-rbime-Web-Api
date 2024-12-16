using System;
using System.Text.Json.Serialization;

public class DietaCreateDto
{

        public string Emri { get; set; } = string.Empty;
        public string Lloji { get; set; } = string.Empty;
        public string Pershkrimi { get; set; }
        [JsonIgnore]
        public DateTime DataKrijimit { get; set; } = DateTime.Now;

}
