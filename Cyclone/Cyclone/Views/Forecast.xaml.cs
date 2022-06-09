using Cyclone.Helper;
using Cyclone.Models;
using Cyclone.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cyclone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Forecast : ContentPage
    {
        public Forecast()
        {
            InitializeComponent();
            ForecastPrediction();
        }

        public async void ForecastPrediction()
        {
            CurrentWeatherPage a = new CurrentWeatherPage();
            var url = $"https://api.openweathermap.org/data/2.5/forecast?q={a.Location}&appid={a.ApiKey}&units={a.Unit}";
            var result = await ApiObj.Get(url);

            if (result.Success)
            {
                try
                {
                    var forecastInfo = JsonConvert.DeserializeObject<ForecastInfo>(result.Response);
                    List<ForecastProperties> items = new List<ForecastProperties>();

                    foreach (var item in forecastInfo.list)
                    {
                        var currentDate = DateTime.Parse(item.dt_txt);

                        if (currentDate > DateTime.Now && currentDate.Hour == 0 && currentDate.Minute == 0 && currentDate.Second == 0)
                        {
                            items.Add(item);
                        }
                    }

                    if (a.Unit == "metric")
                    {
                        firstDayTemperatureUnit.Text = $"°C";
                        secondDayTemperatureUnit.Text = $"°C";
                        thirdDayTemperatureUnit.Text = $"°C";
                        fourthDayTemperatureUnit.Text = $"°C";
                    }
                    else if (a.Unit == "imperial")
                    {
                        firstDayTemperatureUnit.Text = $"°F";
                        secondDayTemperatureUnit.Text = $"°F";
                        thirdDayTemperatureUnit.Text = $"°F";
                        fourthDayTemperatureUnit.Text = $"°F";
                    }

                    firstDay.Text = DateTime.Parse(items[0].dt_txt).ToString("dddd");
                    firstDayDate.Text = DateTime.Parse(items[0].dt_txt).ToString("dd/MM/yyyy");
                    firstDayTemperature.Text = items[0].main.temp_max.ToString();
                    firstDayImg.Source = $"w{items[0].weather[0].icon}";

                    secondDay.Text = DateTime.Parse(items[1].dt_txt).ToString("dddd");
                    secondDayDate.Text = DateTime.Parse(items[1].dt_txt).ToString("dd/MM/yyyy");
                    secondDayTemperature.Text = items[1].main.temp_max.ToString();
                    secondDayImg.Source = $"w{items[1].weather[0].icon}";

                    thirdDay.Text = DateTime.Parse(items[2].dt_txt).ToString("dddd");
                    thirdDayDate.Text = DateTime.Parse(items[2].dt_txt).ToString("dd/MM/yyyy");
                    thirdDayTemperature.Text = items[2].main.temp_max.ToString();
                    thirdDayImg.Source = $"w{items[2].weather[0].icon}";

                    fourthDay.Text = DateTime.Parse(items[3].dt_txt).ToString("dddd");
                    fourthDayDate.Text = DateTime.Parse(items[3].dt_txt).ToString("dd/MM/yyyy");
                    fourthDayTemperature.Text = items[3].main.temp_max.ToString();
                    fourthDayImg.Source = $"w{items[3].weather[0].icon}";
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                await DisplayAlert("Forecast Info", "No Forecast information found", "OK");
            }
        }
    }
}