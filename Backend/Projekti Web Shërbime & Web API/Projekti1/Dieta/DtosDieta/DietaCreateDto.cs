using System;

public class DietaCreateDto
{

        public string Emri { get; set; } = string.Empty;
        public string Lloji { get; set; } = string.Empty;
        public long Pershkrimi {  get; set; }
        public DateTime DataKrijimit { get; set; } = DateTime.Now;

}
