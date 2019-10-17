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

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
