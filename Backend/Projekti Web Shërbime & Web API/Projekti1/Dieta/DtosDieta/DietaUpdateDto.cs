using System;

public class DietaUpdateDto
{
   public int Id { get; set; }
        public string Emri { get; set; } = string.Empty;
        public string Lloji { get; set; } = string.Empty;
        public long Pershkrimi {  get; set; }
        public DateTime DataKrijimit { get; set; } = DateTime.Now;

}
