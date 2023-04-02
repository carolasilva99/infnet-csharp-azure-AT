namespace MVC.Utils;

public class Base64Utils
{
    public static string Base64(IFormFile arquivo)
    {
        using var memoryStream = new MemoryStream();
        arquivo.CopyTo(memoryStream);
        var fileBytes = memoryStream.ToArray();
        var base64 = Convert.ToBase64String(fileBytes);

        return base64;
    }
}