namespace CheckWeather30Mins.Models
{
    public class WeatherInfoApiResponseDTO
    {
        public CurrentWeatherInfo Current { get; set; }
    }

    public class CurrentWeatherInfo
    {
        public float Temperature { get; set; }
    }
}
