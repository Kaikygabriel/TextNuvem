namespace TextNuvem.Infra.Configure;

public class ConfigurationJwt
{
    public string Key { get; init; } = null!;
    public int HoursExpired { get; init; }
}