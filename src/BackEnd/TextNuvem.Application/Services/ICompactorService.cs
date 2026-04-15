namespace TextNuvem.Application.Services;

public interface ICompactorService
{
    string CompressObject<T>(T obj);
    
    T DecompressObject<T>(string obj);
}