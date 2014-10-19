namespace ReceiptScannerOCRS.Contracts
{
    using System.Collections.Generic;
    using System.Drawing;
    
    public interface IReceiptScanner
    {
        IList<string> GetLines(string imagePath, string tessDataPath);

        IList<string> GetLines(Bitmap image, string tessDataPath);
    }
}