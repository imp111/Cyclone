using Cyclone.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Cyclone.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}