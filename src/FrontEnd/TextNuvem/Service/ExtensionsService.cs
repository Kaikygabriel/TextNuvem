namespace TextNuvem.Service;

public static class ExtensionsService
{
       public static string GetLanguage(string fileName)
        {
            if (fileName.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
                return "csharp";
    
            if (fileName.EndsWith(".js", StringComparison.OrdinalIgnoreCase))
                return "javascript";
    
            if (fileName.EndsWith(".ts", StringComparison.OrdinalIgnoreCase))
                return "typescript";
    
            if (fileName.EndsWith(".html", StringComparison.OrdinalIgnoreCase))
                return "html";
    
            if (fileName.EndsWith(".css", StringComparison.OrdinalIgnoreCase))
                return "css";
    
            if (fileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                return "json";
    
            if (fileName.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
                return "markdown";
    
            return "plaintext";
        }

}