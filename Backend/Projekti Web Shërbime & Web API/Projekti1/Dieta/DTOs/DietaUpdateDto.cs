using System;
using System.Text.Json.Serialization;

public class DietaUpdateDto
{
        public int Id { get; set; }
        public string Emri { get; set; } = string.Empty;
        public string Lloji { get; set; } = string.Empty;
        public string Pershkrimi { get; set; } = string.Empty;
        [JsonIgnore]
        public DateTime DataKrijimit { get; set; } = DateTime.Now;
}
