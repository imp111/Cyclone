using Cyclone.Helper;
using Cyclone.Models;
using Cyclone.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            Task.Run(AnimateBackground);
            ForecastPrediction();
        }

        private async void AnimateBackground()
        {
            Action<double> forward = input => bgGradient.AnchorX = input;
            Action<double> backward = input => bgGradient.AnchorX = input;

            while (true)
            {
                bgGradient.Animate(name: "forward", callback: forward, start: 0, end: 1, length: 5000, easing: Easing.SinIn);
                await Task.Delay(3000);
                bgGradient.Animate(name: "backward", callback: backward, start: 1, end: 0, length: 5000, easing: Easing.SinIn);
                await Task.Delay(3000);
            }
        }

        public async void ForecastPrediction()
        {
            CurrentWeatherPage a = new CurrentWeatherPage();
            var url = $"https://api.openweathermap.org/data/2.5/forecast?q={a.Location}&appid={a.ApiKey}&units={a.Unit}";
            //var url = $"https://api.openweathermap.org/data/2.5/forecast/daily?q={a.Location}&units={a.Unit}&cnt={a.Count}&appid={a.ApiKey}";
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
                        fifthDayTemperatureUnit.Text = $"°C";
                        sixthDayTemperatureUnit.Text = $"°C";
                        seventhDayTemperatureUnit.Text = $"°C";
                        eightDayTemperatureUnit.Text = $"°C";
                        ninthDayTemperatureUnit.Text = $"°C";
                        tenthDayTemperatureUnit.Text = $"°C";
                        eleventhDayTemperatureUnit.Text = $"°C";
                    }
                    else if (a.Unit == "imperial")
                    {
                        firstDayTemperatureUnit.Text = $"°F";
                        secondDayTemperatureUnit.Text = $"°F";
                        thirdDayTemperatureUnit.Text = $"°F";
                        fourthDayTemperatureUnit.Text = $"°F";
                        fifthDayTemperatureUnit.Text = $"°F";
                        sixthDayTemperatureUnit.Text = $"°F";
                        seventhDayTemperatureUnit.Text = $"°F";
                        eightDayTemperatureUnit.Text = $"°F";
                        ninthDayTemperatureUnit.Text = $"°F";
                        tenthDayTemperatureUnit.Text = $"°F";
                        eleventhDayTemperatureUnit.Text = $"°F";
                    }

                    firstDay.Text = DateTime.Parse(items[0].dt_txt).ToString("dddd");
                    firstDayDate.Text = DateTime.Parse(items[0].dt_txt).ToString("dd/MM/yyyy");
                    firstDayTemperature.Text = Math.Round(items[0].main.temp_max).ToString();
                    firstDayImg.Source = $"w{items[0].weather[0].icon}.png";

                    secondDay.Text = DateTime.Parse(items[1].dt_txt).ToString("dddd");
                    secondDayDate.Text = DateTime.Parse(items[1].dt_txt).ToString("dd/MM/yyyy");
                    secondDayTemperature.Text = Math.Round(items[1].main.temp_max).ToString();
                    secondDayImg.Source = $"w{items[1].weather[0].icon}.png";

                    thirdDay.Text = DateTime.Parse(items[2].dt_txt).ToString("dddd");
                    thirdDayDate.Text = DateTime.Parse(items[2].dt_txt).ToString("dd/MM/yyyy");
                    thirdDayTemperature.Text = Math.Round(items[2].main.temp_max).ToString();
                    thirdDayImg.Source = $"w{items[2].weather[0].icon}.png";

                    fourthDay.Text = DateTime.Parse(items[3].dt_txt).ToString("dddd");
                    fourthDayDate.Text = DateTime.Parse(items[3].dt_txt).ToString("dd/MM/yyyy");
                    fourthDayTemperature.Text = Math.Round(items[3].main.temp_max).ToString();
                    fourthDayImg.Source = $"w{items[3].weather[0].icon}.png";

                    fifthDay.Text = DateTime.Parse(items[4].dt_txt).ToString("dddd");
                    fifthDayDate.Text = DateTime.Parse(items[4].dt_txt).ToString("dd/MM/yyyy");
                    fifthDayTemperature.Text = Math.Round(items[4].main.temp_max).ToString();
                    fifthDayImg.Source = $"w{items[4].weather[0].icon}.png";

                    /*

                    sixthDay.Text = DateTime.Parse(items[5].dt_txt).ToString("dddd");
                    sixthDayDate.Text = DateTime.Parse(items[5].dt_txt).ToString("dd/MM/yyyy");
                    sixthDayTemperature.Text = Math.Round(items[5].main.temp_max).ToString();
                    sixthDayImg.Source = $"w{items[5].weather[0].icon}.png";

                    seventhDay.Text = DateTime.Parse(items[6].dt_txt).ToString("dddd");
                    seventhDayDate.Text = DateTime.Parse(items[6].dt_txt).ToString("dd/MM/yyyy");
                    seventhDayTemperature.Text = Math.Round(items[6].main.temp_max).ToString();
                    seventhDayImg.Source = $"w{items[6].weather[0].icon}.png";

                    eightDay.Text = DateTime.Parse(items[7].dt_txt).ToString("dddd");
                    eightDayDate.Text = DateTime.Parse(items[7].dt_txt).ToString("dd/MM/yyyy");
                    eightDayTemperature.Text = Math.Round(items[7].main.temp_max).ToString();
                    eightDayImg.Source = $"w{items[7].weather[0].icon}.png";

                    ninthDay.Text = DateTime.Parse(items[8].dt_txt).ToString("dddd");
                    ninthDayDate.Text = DateTime.Parse(items[8].dt_txt).ToString("dd/MM/yyyy");
                    ninthDayTemperature.Text = Math.Round(items[8].main.temp_max).ToString();
                    ninthDayImg.Source = $"w{items[8].weather[0].icon}.png";

                    tenthDay.Text = DateTime.Parse(items[9].dt_txt).ToString("dddd");
                    tenthDayDate.Text = DateTime.Parse(items[9].dt_txt).ToString("dd/MM/yyyy");
                    tenthDayTemperature.Text = Math.Round(items[9].main.temp_max).ToString();
                    tenthDayImg.Source = $"w{items[9].weather[0].icon}.png";

                    eleventhDay.Text = DateTime.Parse(items[10].dt_txt).ToString("dddd");
                    eleventhDayDate.Text = DateTime.Parse(items[10].dt_txt).ToString("dd/MM/yyyy");
                    eleventhDayTemperature.Text = Math.Round(items[10].main.temp_max).ToString();
                    eleventhDayImg.Source = $"w{items[10].weather[0].icon}.png";
                    */
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