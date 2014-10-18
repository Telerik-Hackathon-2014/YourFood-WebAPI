namespace ReceiptScannerOCRS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Tesseract;

    public class ReceiptScannerTesseract
    {
        private const string TessDataPath = "../../../ReceiptScannerOCRS/tessdata";
        private const string ImagePath = "../../../ReceiptScannerOCRS/fake-receipt.jpg";
        
        public IList<string> GetLines(string imagePath = ImagePath)
        {
            using (var engine = new TesseractEngine(TessDataPath, "eng", EngineMode.Default))
            {
                using (var image = Pix.LoadFromFile(ImagePath))
                {
                    using (var page = engine.Process(image))
                    {
                        return this.ExtractLinesAsStrings(page);
                    }
                }
            }
        }
 
        private IList<string> ExtractLinesAsStrings(Page page)
        {
            var lines = new List<string>();

            var text = page.GetText()
                           .Trim()
                           .ToLower()
                           .Split(new char[] { '\r', '\n' });

            foreach (var entry in text)
            {
                var trimmedLine = entry.Trim();

                if (!string.IsNullOrEmpty(trimmedLine))
                {
                    lines.Add(trimmedLine);
                }
            }

            return lines;
        }
    }
}