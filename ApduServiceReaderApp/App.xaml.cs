using System.Threading.Tasks;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ApduServiceReaderApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }

        public static async Task DisplayAlertAsync(string msg) =>
            await Device.InvokeOnMainThreadAsync(async () => await Current.MainPage.DisplayAlert("message from service", msg, "ok"));
    }
}
