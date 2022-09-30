using Moq;

namespace HouseControl.Sunset.Tests;

public class Tests
{
    private string goodData = "{\"results\":{\"sunrise\":\"4:52:36 AM\",\"sunset\":\"3:53:04 PM\",\"solar_noon\":\"10:22:50 AM\",\"day_length\":\"11:00:28.2563158\"},\"status\":\"OK\"}";
    private string badData = "{\"results\":null,\"status\":\"ERROR\"}";

    [Test]
    public void SunsetProvider_ImplementInterface()
    {
        var provider = new SolarServiceSunsetProvider();
        Assert.IsInstanceOf<ISunsetProvider>(provider);
    }

    [Test]
    public void ToLocalTime_OnValidTimeString_ReturnsExpectedTime()
    {
        // Arrange
        var provider = new SolarServiceSunsetProvider();
        string timeString = "3:53:04 PM";
        DateOnly date = new DateOnly(2022, 10, 20);
        DateTimeOffset expected = new DateTime(2022, 10, 20, 15, 53, 04);

        // Act
        DateTimeOffset result = provider.ToLocalTime(date, timeString);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ParseSunriseTime_OnValidData_ReturnsExpectedTimeString()
    {
        var provider = new SolarServiceSunsetProvider();
        string expected = "4:52:36 AM";

        string result = provider.ParseSunriseTime(goodData);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ParseSunriseTime_OnErrorData_ThrowsArgumentException()
    {
        var provider = new SolarServiceSunsetProvider();

        try
        {
            provider.ParseSunriseTime(badData);
            Assert.Fail("ArgumentException not thrown");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }

    [Test]
    public void ParseSunsetTime_OnValidData_ReturnsExpectedTimeString()
    {
        var provider = new SolarServiceSunsetProvider();
        string expected = "3:53:04 PM";

        string result = provider.ParseSunsetTime(goodData);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ParseSunsetTime_OnErrorData_ThrowsArgumentException()
    {
        var provider = new SolarServiceSunsetProvider();

        try
        {
            provider.ParseSunsetTime(badData);
            Assert.Fail("ArgumentException not thrown");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
    }

    [Test]
    public void CheckStatus_OnValidStatus_ReturnsTrue()
    {
        var provider = new SolarServiceSunsetProvider();
        bool result = provider.CheckStatus(goodData);
        Assert.IsTrue(result);
    }

    [Test]
    public void CheckStatus_OnErrorStatus_ReturnsFalse()
    {
        var provider = new SolarServiceSunsetProvider();
        bool result = provider.CheckStatus(badData);
        Assert.IsFalse(result);
    }

    [Test]
    public async Task GetSunset_OnValidDate_ReturnsExpectedDateTime()
    {
        Mock<ISolarService> serviceMock = new();
        serviceMock.Setup(s => s.GetServiceData(It.IsAny<DateOnly>()))
            .Returns(Task.FromResult(goodData));

        var provider = new SolarServiceSunsetProvider();
        provider.Service = serviceMock.Object;

        DateOnly date = new DateOnly(2022, 10, 20);
        DateTimeOffset expected = new DateTime(2022, 10, 20, 15, 53, 04);

        DateTimeOffset result = await provider.GetSunset(date);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetSunrise_OnValidDate_ReturnsExpectedDateTime()
    {
        Mock<ISolarService> serviceMock = new();
        serviceMock.Setup(s => s.GetServiceData(It.IsAny<DateOnly>()))
            .Returns(Task.FromResult(goodData));

        var provider = new SolarServiceSunsetProvider();
        provider.Service = serviceMock.Object;

        DateOnly date = new DateOnly(2022, 10, 20);
        DateTimeOffset expected = new DateTime(2022, 10, 20, 04, 52, 36);

        DateTimeOffset result = await provider.GetSunrise(date);

        Assert.That(result, Is.EqualTo(expected));
    }
}