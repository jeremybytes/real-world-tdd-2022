using HouseControl.Sunset;

namespace SunsetTestApp;

public class Program
{
    public static async Task Main()
    {
        ISunsetProvider provider = new SolarServiceSunsetProvider();
        var today = DateOnly.FromDateTime(DateTime.Today);
        var sunrise = await provider.GetSunrise(today);
        var sunset = await provider.GetSunset(today);
        Console.WriteLine("Today's Sunrise/Sunset Times:");
        Console.WriteLine($"Date: {today}");
        Console.WriteLine($"Sunrise: {sunrise}");
        Console.WriteLine($"Sunset: {sunset}");
    }
}