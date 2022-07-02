using System;
using Xamarin.Forms;
using Newtonsoft.Json;
using Cyclone.Helper;
using Cyclone.Models;
using System.Collections.Generic;
using Cyclone.ViewModels;

namespace Cyclone.Views
{
    public partial class CurrentWeatherPage : ContentPage
    {
        internal string ApiKey = "0668feb2839853a4357b33fa849d4cc5";
        internal string Location = "";
        internal string Unit = ""; //imperial and metric

        public CurrentWeatherPage()
        {
            InitializeComponent();
            ApiWeatherResponse();
        }

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
                    weatherDate.Text = date.ToString("dddd, dd/MM").ToUpper();
                    weatherTemperature.Text = $"{Math.Round(weatherInfo.main.temp)}";

                    //weatherTime.Text = zone.ToString("HH/mm/ss");

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