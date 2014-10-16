namespace YourFood.EverliveAPI.Contracts
{
    public interface IImageUploader
    {
        string UrlFromBase64Image(string base64);
    }
}