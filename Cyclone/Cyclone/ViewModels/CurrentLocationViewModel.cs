using Cyclone.Views;
using Newtonsoft.Json;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Cyclone.Models;

namespace Cyclone.ViewModels
{
    public class CurrentLocationViewModel : BaseViewModel
    {
        public CurrentLocationViewModel()
        {
            Title = "Weather";
            BgSource = "bgLondon.png";
        }
    }
}