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
            //ForecastPrediction();
        }

        private string Location = "Stara Zagora";
        private string ApiKey = "0668feb2839853a4357b33fa849d4cc5";
        private string Unit = "imperial"; //imperial and metric

        private async void ApiWeatherResponse()
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={Location}&appid={ApiKey}&units={Unit}";
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

                    weatherDate.Text = date.ToString("dddd, dd/MM/yyyy").ToUpper();
                    weatherTemperature.Text = $"{weatherInfo.main.temp}";

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

        //public async void ForecastPrediction()
        //{
        //    var url = $"https://api.openweathermap.org/data/2.5/forecast?q={Location}&appid={ApiKey}&units={Unit}";
        //    var result = await ApiObj.Get(url);

        //    if (result.Success)
        //    {
        //        try
        //        {
        //            var forecastInfo = JsonConvert.DeserializeObject<Forecast>(result.Response);
        //            List<List> items = new List<List>();

        //            foreach (var item in forecastInfo.list)
        //            {
        //                var currentDate = DateTime.Parse(item.dt_txt);

        //                if (currentDate > DateTime.Now && currentDate.Hour == 0 && currentDate.Minute == 0 && currentDate.Second == 0)
        //                {
        //                    items.Add(item);
        //                }

        //                firstDay.Text = DateTime.Parse(items[0].dt_txt).ToString("dddd");
        //                firstDayDate.Text = DateTime.Parse(items[0].dt_txt).ToString("dddd/MM/yyyy");
        //                firstDayTemperature.Text = items[0].main.temp.ToString();

        //                if (Unit == "metric")
        //                {
        //                    firstDayTemperatureUnit.Text = $"°C";
        //                }
        //                else if (Unit == "imperial")
        //                {
        //                    firstDayTemperatureUnit.Text = $"°F";
        //                }
                        
        //                //firstDayImg.Source = ;
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //    }
        //    else
        //    {
        //        await DisplayAlert("Weather Info", "No Weather information found", "OK");
        //    }
        //}
    }
}