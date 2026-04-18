using System.IO.Compression;
using System.Text;
using System.Text.Json;
using TextNuvem.Application.Services;

namespace TextNuvem.Infra.Services;

internal sealed class CompactorService : ICompactorService
{
    public string CompressObject<T>(T obj)
    {
        var json = JsonSerializer.Serialize(obj, new JsonSerializerOptions
        {
            IncludeFields = true,
            WriteIndented = true,
        });
        
        byte[] data = Encoding.UTF8.GetBytes(json);

        using var output = new MemoryStream();
        
        using (var gzip = new GZipStream(output, CompressionLevel.Optimal))
        {
            gzip.Write(data, 0, data.Length);
        }
        return Convert.ToBase64String(output.ToArray());
    }

    public T DecompressObject<T>(string obj)
    {
        var compressedData = Convert.FromBase64String(obj);
        using var input = new MemoryStream(compressedData);
        using var gzip = new GZipStream(input, CompressionMode.Decompress);
        using var output = new MemoryStream();
        
        gzip.CopyTo(output);
        
        string json = Encoding.UTF8.GetString(output.ToArray());

        if (string.IsNullOrEmpty(json))
            return Activator.CreateInstance<T>();
        
        return JsonSerializer.Deserialize<T>(json) 
               ?? Activator.CreateInstance<T>();
        
    }
}