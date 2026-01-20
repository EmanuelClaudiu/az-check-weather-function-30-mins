using CheckWeather30Mins.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace CheckWeather30Mins;

public class CheckWeatherEvery30Minutes
{
    private readonly ILogger _logger;
    private readonly HttpClient _httpClient;

    public CheckWeatherEvery30Minutes(ILoggerFactory loggerFactory, HttpClient httpClient)
    {
        _logger = loggerFactory.CreateLogger<CheckWeatherEvery30Minutes>();
        _httpClient = httpClient;
    }

    [Function("CheckWeatherEvery30Minutes")]
    public async Task Run([TimerTrigger("0 */30 * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation("[CheckWeatherEvery30Minutes] Checking weather API...");

        var weatherApiBaseUrl = Environment.GetEnvironmentVariable("AZ_WEATHER_INFO_API_CONTAINER_BASE_URL");
        var response = await _httpClient.GetFromJsonAsync<WeatherInfoApiResponseDTO>($"{weatherApiBaseUrl}/Weather/Bistrita" ?? "");
        
        if (response != null)
        {
            _logger.LogInformation($"[CheckWeatherEvery30Minutes] Weather in Bistrita is: {response.Current.Temperature}");
        }
        else
        {
            _logger.LogError("[CheckWeatherEvery30Minutes] Failed to reach the Weather API");
        }
    }
}