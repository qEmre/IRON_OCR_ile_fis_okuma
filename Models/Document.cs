using System.ComponentModel.DataAnnotations;

namespace OCRproject.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string ResimAdi { get; set; }
        public string ResimUrl { get; set; }
        public string FirmaAdi { get; set; }
        public string Adres { get; set; }
        public string VergiNo { get; set; }
        public DateTime Tarih { get; set; }
        public string FisNo { get; set; }
        public decimal KDV { get; set; }
        public decimal Toplam { get; set; }
        public virtual ICollection<documentContent> DocumentContents { get; set; }
    }
}