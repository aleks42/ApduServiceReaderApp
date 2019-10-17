using Android.App;
using Android.Content.PM;
using Android.Nfc;
using Android.OS;
using Android.Runtime;
using ApduServiceReaderApp.Droid.Services;

namespace ApduServiceReaderApp.Droid
{
    [Activity(Label = "ApduServiceReaderApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public CardReader cardReader;
        
        public NfcReaderFlags READER_FLAGS = NfcReaderFlags.NfcA | NfcReaderFlags.SkipNdefCheck;

        private void EnableReaderMode()
        {
            var nfc = NfcAdapter.GetDefaultAdapter(this);
            if (nfc != null) nfc.EnableReaderMode(this, cardReader, READER_FLAGS, null);
        }

        private void DisableReaderMode()
        {
            var nfc = NfcAdapter.GetDefaultAdapter(this);
            if (nfc != null) nfc.DisableReaderMode(this);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            cardReader = new CardReader();
            EnableReaderMode();

            LoadApplication(new App());
        }

        protected override void OnPause()
        {
            base.OnPause();
            DisableReaderMode();
        }

        protected override void OnResume()
        {
            base.OnResume();
            EnableReaderMode();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}