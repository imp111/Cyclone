using System;
using Xamarin.Forms;
using Newtonsoft.Json;
using Cyclone.Helper;
using Cyclone.Models;
using System.Collections.Generic;

namespace Cyclone.Views
{
    public partial class CurrentWeatherPage : ContentPage
    {
        public CurrentWeatherPage()
        {
            InitializeComponent();
            ApiWeatherResponse();
        }

        internal string Location = "Stara Zagora";
        internal string ApiKey = "0668feb2839853a4357b33fa849d4cc5";
        internal string Unit = "metric"; //imperial and metric

        private async void ApiWeatherResponse()
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={Location}&appid={ApiKey}&units={Unit}";
            var result = await ApiObj.Get(url);

            if (result.Success)
            {
                try
                {
                    var weatherInfo = JsonConvert.DeserializeObject<Rootobject>(result.Response);

                    weatherConditionText.Text = weatherInfo.weather[0].description.ToUpper();
                    weatherLocation.Text = $"{Location}, {weatherInfo.sys.country.ToUpper()}";
                    weatherConditionImage.Source = $"w{weatherInfo.weather[0].icon}";

                    var date = new DateTime().ToUniversalTime().AddSeconds(weatherInfo.dt);

                    weatherDate.Text = date.ToString("dddd, dd/MM/yyyy").ToUpper();
                    weatherTemperature.Text = $"{Math.Round(weatherInfo.main.temp)}";

                    if (Unit == "metric")
                    {
                        weatherTemperatureUnitSign.Text = $"°C";
                    }
                    else if (Unit == "imperial")
                    {
                        weatherTemperatureUnitSign.Text = $"°F";
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