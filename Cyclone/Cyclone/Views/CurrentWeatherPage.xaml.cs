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

        private string Location = "Radnevo";
        private string Unit = "imperial";

        private async void ApiWeatherResponse()
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={Location}&appid=0668feb2839853a4357b33fa849d4cc5&units={Unit}";
            var result = await ApiObj.Get(url);

            if (result.Success)
            {
                try
                {
                    var weatherInfo = JsonConvert.DeserializeObject<Rootobject>(result.Response);
                    weatherConditionText.Text = weatherInfo.weather[0].description.ToUpper();
                    weatherLocation.Text = $"{Location}, {weatherInfo.sys.country.ToUpper()}";
                    //weatherDate.Text = "";
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