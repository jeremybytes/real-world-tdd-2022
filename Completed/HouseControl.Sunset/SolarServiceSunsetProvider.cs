using Microsoft.CSharp.RuntimeBinder;
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
        string sunriseTimeString = ParseSunriseTime(data);
        DateTimeOffset result = ToLocalTime(date, sunriseTimeString);
        return result;
    }

    public async Task<DateTimeOffset> GetSunset(DateOnly date)
    {
        string data = await Service.GetServiceData(date);
        string sunsetTimeString = ParseSunsetTime(data);
        DateTimeOffset result = ToLocalTime(date, sunsetTimeString);
        return result;
    }

    public bool CheckStatus(string jsonData)
    {
        dynamic? data = JsonConvert.DeserializeObject(jsonData);
        return data!.status == "OK";
    }

    public string ParseSunsetTime(string jsonData)
    {
        if (!CheckStatus(jsonData))
            throw new ArgumentException(jsonData);

        dynamic? data = JsonConvert.DeserializeObject(jsonData);
        return data!.results.sunset;
    }

    public DateTimeOffset ToLocalTime(DateOnly date, string timeString)
    {
        TimeOnly time = TimeOnly.Parse(timeString);
        DateTimeOffset result = date.ToDateTime(time);
        return result;
    }

    public string ParseSunriseTime(string jsonData)
    {
        if (!CheckStatus(jsonData))
            throw new ArgumentException(jsonData);

        dynamic? data = JsonConvert.DeserializeObject(jsonData);
        return data!.results.sunrise;

    }
}
