using Newtonsoft.Json;

namespace HouseControl.Sunset;

public class SolarServiceSunsetProvider : ISunsetProvider
{
    private ISolarService? service;
    public ISolarService Service
    {
        get => service ??= new SolarService();
        set => service = value;
    }

    public async Task<DateTimeOffset> GetSunrise(DateOnly date)
    {
        string data = await Service.GetServiceData(date);
        string sunriseTimeString = ParseSunriseString(data);
        DateTimeOffset result = ToLocalTime(date, sunriseTimeString);
        return result;
    }

    public async Task<DateTimeOffset> GetSunset(DateOnly date)
    {
        string data = await Service.GetServiceData(date);
        string sunsetTimeString = ParseSunsetString(data);
        DateTimeOffset result = ToLocalTime(date, sunsetTimeString);
        return result;
    }

    public DateTimeOffset ToLocalTime(DateOnly targetDate, string timeString)
    {
        TimeOnly time = TimeOnly.Parse(timeString);
        DateTimeOffset result = targetDate.ToDateTime(time);
        return result;
    }

    public bool CheckStatus(string inputString)
    {
        dynamic? data = JsonConvert.DeserializeObject(inputString);
        return data?.status == "OK";
    }

    public string ParseSunsetString(string inputString)
    {
        if (!CheckStatus(inputString))
            throw new ArgumentException("Invalid input string");
        dynamic? data = JsonConvert.DeserializeObject(inputString);
        return data!.results.sunset;
    }

    public string ParseSunriseString(string inputString)
    {
        if (!CheckStatus(inputString))
            throw new ArgumentException("Invalid input string");
        dynamic? data = JsonConvert.DeserializeObject(inputString);
        return data!.results.sunrise;
    }
}
