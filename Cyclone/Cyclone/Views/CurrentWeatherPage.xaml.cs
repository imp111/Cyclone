using System;
using Xamarin.Forms;
using Newtonsoft.Json;
using Cyclone.Helper;
using Cyclone.Models;

namespace Cyclone.Views
{
    public partial class CurrentWeatherPage : ContentPage
    {
        public CurrentWeatherPage()
        {
            InitializeComponent();
            ApiWeatherResponse();
        }

        private string Location = "Los Angeles";
        private string Unit = "metric"; //imperial and metric

        private async void ApiWeatherResponse()
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={Location}&appid=0668feb2839853a4357b33fa849d4cc5&units={Unit}";
            var result = await ApiObj.Get(url);

            if (result.Success)
            {
                try
                {
                    var weatherInfo = JsonConvert.DeserializeObject<Rootobject>(result.Response);

                    if (weatherInfo.weather[0].id >= 200 && weatherInfo.weather[0].id <= 232)
                    {
                        weatherConditionImage.Source = "thunderstorm.png";
                    }
                    else if (weatherInfo.weather[0].id >= 300 && weatherInfo.weather[0].id <= 321)
                    {
                        weatherConditionImage.Source = "showerRain.png";
                    }
                    else if (weatherInfo.weather[0].id >= 500 && weatherInfo.weather[0].id <= 531)
                    {
                        if (weatherInfo.weather[0].icon[2] == 'd')
                        {
                            weatherConditionImage.Source = "rain.png";
                        }
                        else if (weatherInfo.weather[0].icon[2] == 'n')
                        {
                            weatherConditionImage.Source = "showerRain.png";
                        }
                    }
                    else if (weatherInfo.weather[0].id >= 600 && weatherInfo.weather[0].id <= 622)
                    {
                        weatherConditionImage.Source = "snow.png";
                    }
                    else if (weatherInfo.weather[0].id >= 701 && weatherInfo.weather[0].id <= 781)
                    {
                        weatherConditionImage.Source = "fog.png";
                    }
                    else if (weatherInfo.weather[0].id == 800)
                    {
                        if (weatherInfo.weather[0].icon[2] == 'd')
                        {
                            weatherConditionImage.Source = "sun.png";
                        }
                        else if (weatherInfo.weather[0].icon[2] == 'n')
                        {
                            weatherConditionImage.Source = "night.png";
                        }
                    }
                    else if (weatherInfo.weather[0].id == 801)
                    {
                        if (weatherInfo.weather[0].icon[2] == 'd')
                        {
                            weatherConditionImage.Source = "fewCloudsDay.png";
                        }
                        else if (weatherInfo.weather[0].icon[2] == 'n')
                        {
                            weatherConditionImage.Source = "fewCloudsNight.png";
                        }
                    }
                    else if (weatherInfo.weather[0].id == 802)
                    {
                        if (weatherInfo.weather[0].icon[2] == 'd')
                        {
                            weatherConditionImage.Source = "scatteredCloudsDay.png";
                        }
                        else if (weatherInfo.weather[0].icon[2] == 'n')
                        {
                            weatherConditionImage.Source = "scatteredCloudsNight.png";
                        }
                    }
                    else if (weatherInfo.weather[0].id == 803)
                    {
                        if (weatherInfo.weather[0].icon[2] == 'd')
                        {
                            weatherConditionImage.Source = "brokenCloudsDay.png";
                        }
                        else if (weatherInfo.weather[0].icon[2] == 'n')
                        {
                            weatherConditionImage.Source = "brokenCloudsNight.png";
                        }
                    }
                    else if (weatherInfo.weather[0].id == 804)
                    {
                        if (weatherInfo.weather[0].icon[2] == 'd')
                        {
                            weatherConditionImage.Source = "brokenClouds.png";
                        }
                        else if (weatherInfo.weather[0].icon[2] == 'n')
                        {
                            weatherConditionImage.Source = "brokenClouds.png";
                        }
                    }

                    weatherConditionText.Text = weatherInfo.weather[0].description.ToUpper();
                    weatherLocation.Text = $"{Location}, {weatherInfo.sys.country.ToUpper()}";

                    var date = new DateTime().ToUniversalTime().AddSeconds(weatherInfo.dt);

                    weatherDate.Text = date.ToString("dddd, MM, dd").ToUpper();
                    weatherTemperature.Text = $"{weatherInfo.main.temp}";

                    if (Unit == "metric")
                    {
                        weatherTemperatureUnitSign.Text = "°";
                        weatherTemperatureUnit.Text = $"Celcius";
                    }
                    else if (Unit == "imperial")
                    {
                        weatherTemperatureUnitSign.Text = "F";
                        weatherTemperatureUnit.Text = $"Fahrenheit";
                    }

                    weatherHumidityPercentage.Text = $"{weatherInfo.main.humidity}%";
                    weatherWindSpeed.Text = $"{weatherInfo.wind.speed} m/s";
                    weatherPressure.Text = $"{weatherInfo.main.pressure} hpa";
                    weatherCloudinessPercentage.Text = $"{weatherInfo.clouds.all}%";
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                await DisplayAlert("Weather Info", "No Weather information found", "OK");
            }
        }
    }
}