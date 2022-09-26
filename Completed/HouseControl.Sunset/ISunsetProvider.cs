namespace HouseControl.Sunset;

public interface ISunsetProvider
{
    Task<DateTimeOffset> GetSunset(DateOnly date);
    Task<DateTimeOffset> GetSunrise(DateOnly date);
}
