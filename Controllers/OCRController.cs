using Microsoft.AspNetCore.Mvc;
using IronOcr;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using OCRproject.DataLayer;
using OCRproject.Models;

namespace projectOCR.Controllers
{
    public class OCRController : Controller
    {
        private readonly ProjectDbContext _projectDbContext;

        public OCRController(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult documentSave(IFormFile file, string imageName)
        {
            // resmin uzantısını al ve yeni dosya adı oluştur
            var extension = Path.GetExtension(file.FileName);
            var newImage = $"{Guid.NewGuid()}{extension}";
            var kayitYeri = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", newImage);

            using (var stream = new FileStream(kayitYeri, FileMode.Create))
            {
                file.CopyTo(stream); // belirlenen konuma resmi kaydeder
            }

            // OCR işlemi
            var Ocr = new IronTesseract();
            Ocr.Language = OcrLanguage.Turkish;
            string ocrResult;

            using (var Input = new OcrInput())
            {
                Input.AddImage(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", newImage));
                Input.Deskew(); // görüntü hizalama
                Input.DeNoise(); // görüntü temizleme
                ocrResult = Ocr.Read(Input).Text; // düzenlenen metni okuma
            }

            DateTime tarih;
            string[] satirlar = ocrResult.Split('\n');
            string firmaAdi = satirlar[0] + "" + satirlar[1];
            string adres = satirlar[2] + " " + satirlar[3];
            string vergiNo = satirlar[4].Replace("S.BEYLİ VD.", "").Trim();
            string tarihStr = satirlar[6].Replace("TARİH !", "").Trim();
            DateTime.TryParse(tarihStr, out tarih);
            string fisNo = new string(satirlar[10].Where(char.IsDigit).ToArray());
            string urunBilgisi = new string(satirlar[12].Where(char.IsLetter).ToArray()).Trim();
            string urunFiyati = satirlar[12].Split('*')[1].Trim();
            urunFiyati = urunFiyati.Replace(" ", "");
            decimal urunFiyat = Convert.ToDecimal(urunFiyati);
            decimal toplamKDV = decimal.TryParse(Regex.Match(satirlar[13], @"\d+,\d+").Value.Trim(), out decimal kdv) ? kdv : 0;
            decimal toplam = decimal.TryParse(Regex.Match(satirlar[14], @"\d+,\d+").Value.Trim(), out decimal toplamDeger) ? toplamDeger : 0;

            Document document = new Document
            {
                ResimAdi = imageName,
                ResimUrl = "/img/" + newImage,
                FirmaAdi = firmaAdi,
                Adres = adres,
                VergiNo = vergiNo,
                Tarih = tarih,
                FisNo = fisNo,
                KDV = toplamKDV,
                Toplam = toplam,
                DocumentContents = new List<documentContent>()
            };
            _projectDbContext.documentTable.Add(document);
            _projectDbContext.SaveChanges();

            documentContent documentContent = new documentContent
            {
                UrunBilgisi = urunBilgisi,
                UrunFiyati = urunFiyat,
                DocumentId = document.Id
            };
            _projectDbContext.documentContentTable.Add(documentContent);
            _projectDbContext.SaveChanges();

            return RedirectToAction("documentList", "OCR");
        }

        public IActionResult documentList()
        {
            List<Document> documentList = _projectDbContext.documentTable.ToList();

            return View(documentList);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var delete = _projectDbContext.documentTable.FirstOrDefault(i => i.Id == id);

            _projectDbContext.documentTable.Remove(delete);
            _projectDbContext.SaveChanges();

            return RedirectToAction("documentList", "OCR");
        }

        public IActionResult getDocument(int? id)
        {
            var documentGet = _projectDbContext.documentTable.Include(d => d.DocumentContents).FirstOrDefault(d => d.Id == id);

            return View(documentGet);
        }
    }
}