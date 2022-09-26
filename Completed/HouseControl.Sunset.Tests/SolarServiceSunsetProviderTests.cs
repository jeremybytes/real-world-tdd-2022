using Moq;

namespace HouseControl.Sunset.Tests;

public class Tests
{
    string goodResult = "{\"results\":{\"sunrise\":\"4:52:36 AM\",\"sunset\":\"3:53:04 PM\",\"solar_noon\":\"10:22:50 AM\",\"day_length\":\"11:00:28.2563158\"},\"status\":\"OK\"}";
    string badResult = "{\"results\":null,\"status\":\"ERROR\"}";

    [Test]
    public void SolarServiceProvider_ImplementsProviderInterface()
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
        DateOnly targetDate = new DateOnly(2022, 10, 22);
        DateTimeOffset expected = new DateTime(2022, 10, 22, 15, 53, 04);

        // Act
        DateTimeOffset result = provider.ToLocalTime(targetDate, timeString);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void CheckStatus_OnValidStatus_ReturnsTrue()
    {
        var provider = new SolarServiceSunsetProvider();
        bool result = provider.CheckStatus(goodResult);
        Assert.IsTrue(result);
    }

    [Test]
    public void CheckStatus_OnErrorStatus_ReturnsFalse()
    {
        var provider = new SolarServiceSunsetProvider();
        bool result = provider.CheckStatus(badResult);
        Assert.IsFalse(result);
    }

    [Test]
    public void ParseSunsetString_OnValidData_ReturnsExpectedValue()
    {
        var provider = new SolarServiceSunsetProvider();
        string expected = "3:53:04 PM";
        string result = provider.ParseSunsetString(goodResult);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ParseSunsetString_OnErrorData_ThrowsArgumentException()
    {
        var provider = new SolarServiceSunsetProvider();
        try
        {
            provider.ParseSunsetString(badResult);
            Assert.Fail("ArgumentException not thrown");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
        catch (Exception ex)
        {
            Assert.Fail($"Exception other than ArgumentException thrown: {ex.GetType()}");
        }
    }

    [Test]
    public void ParseSunriseString_OnValidData_ReturnsExpectedValue()
    {
        var provider = new SolarServiceSunsetProvider();
        string expected = "4:52:36 AM";
        string result = provider.ParseSunriseString(goodResult);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ParseSunriseString_OnErrorData_ThrowsArgumentException()
    {
        var provider = new SolarServiceSunsetProvider();
        try
        {
            provider.ParseSunriseString(badResult);
            Assert.Fail("ArgumentException not thrown");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
        catch (Exception ex)
        {
            Assert.Fail($"Exception other than ArgumentException thrown: {ex.GetType()}");
        }
    }

    [Test]
    public async Task GetSunset_OnValidDate_ReturnsExpectedTime()
    {
        Mock<ISolarService> serviceMock = new Mock<ISolarService>();
        serviceMock.Setup(s => s.GetServiceData(It.IsAny<DateOnly>()))
            .Returns(Task.FromResult(goodResult));

        var provider = new SolarServiceSunsetProvider();
        provider.Service = serviceMock.Object;

        DateOnly targetDate = new DateOnly(2022, 10, 22);
        DateTimeOffset expected = new DateTime(2022, 10, 22, 15, 53, 04);

        DateTimeOffset result = await provider.GetSunset(targetDate);

        Assert.That(result, Is.EqualTo(expected));

    }

    [Test]
    public async Task GetSunrise_OnValidDate_ReturnsExpectedTime()
    {
        Mock<ISolarService> serviceMock = new Mock<ISolarService>();
        serviceMock.Setup(s => s.GetServiceData(It.IsAny<DateOnly>()))
            .Returns(Task.FromResult(goodResult));

        var provider = new SolarServiceSunsetProvider();
        provider.Service = serviceMock.Object;

        DateOnly targetDate = new DateOnly(2022, 10, 22);
        DateTimeOffset expected = new DateTime(2022, 10, 22, 04, 52, 36);

        DateTimeOffset result = await provider.GetSunrise(targetDate);

        Assert.That(result, Is.EqualTo(expected));

    }

}