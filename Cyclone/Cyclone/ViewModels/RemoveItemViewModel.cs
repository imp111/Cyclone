using Cyclone.Models;
using System;
using Cyclone.Views;

using Xamarin.Forms;

namespace Cyclone.ViewModels
{
    internal class RemoveItemViewModel : BaseViewModel
    {
        private string text;
        private string unit;
        public Command RemoveCommand { get; }
        public Command CancelCommand { get; }

        public RemoveItemViewModel()
        {
            RemoveCommand = new Command(OnRemove, ValidateRemove);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => RemoveCommand.ChangeCanExecute();
        }

        private bool ValidateRemove()
        {
            return !String.IsNullOrWhiteSpace(text);
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

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
        }

        private async void OnRemove()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Text = Text,
                Unit = Unit
            };

            await DataStore.DeleteItemAsync(newItem.Text);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
        }
    }
}