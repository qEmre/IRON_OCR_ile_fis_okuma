using System.ComponentModel.DataAnnotations;

namespace OCRproject.Models
{
    public class documentContent
    {
        [Key]
        public int DocumentConentId { get; set; }
        public string UrunBilgisi { get; set; }
        public decimal UrunFiyati { get; set; }
        public int DocumentId { get; set; }
        public virtual Document Document { get; set; }
    }
}