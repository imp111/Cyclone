using Cyclone.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Cyclone.Views;

namespace Cyclone.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string unit;
        public string Id { get; set; }
        public Command SelectItemCommand { get; }

        public ItemDetailViewModel()
        {
            SelectItemCommand = new Command(OnSelect);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Unit
        {
            get => unit;
            set => SetProperty(ref unit, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void OnSelect()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync($"//{nameof(CurrentWeatherPage)}");
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Unit = item.Unit;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}