using Cyclone.Models;
using Cyclone.ViewModels;
using Cyclone.Views;
using System;
using Xamarin.Forms;

namespace Cyclone.Views
{
    public partial class RemoveItemPage : ContentPage
    {
        public Item Item { get; set; }

        public RemoveItemPage()
        {
            InitializeComponent();
            BindingContext = new RemoveItemViewModel();
        }
    }
}