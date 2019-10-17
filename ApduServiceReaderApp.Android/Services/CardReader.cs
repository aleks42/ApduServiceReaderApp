using Android.Nfc;
using Android.Nfc.Tech;
using System;
using System.Linq;
using System.Text;

namespace ApduServiceReaderApp.Droid.Services
{
    public class CardReader : Java.Lang.Object, NfcAdapter.IReaderCallback
    {
        // ISO-DEP command HEADER for selecting an AID.
        // Format: [Class | Instruction | Parameter 1 | Parameter 2]
        private static readonly byte[] SELECT_APDU_HEADER = new byte[] { 0x00, 0xA4, 0x04, 0x00 };

        // AID for our loyalty card service.
        private static readonly string SAMPLE_LOYALTY_CARD_AID = "F123456789";

        // "OK" status word sent in response to SELECT AID command (0x9000)
        private static readonly byte[] SELECT_OK_SW = new byte[] { 0x90, 0x00 };

        public async void OnTagDiscovered(Tag tag)
        {
            IsoDep isoDep = IsoDep.Get(tag);

            if (isoDep != null)
            {
                try
                {
                    isoDep.Connect();

                    var aidLength = (byte)(SAMPLE_LOYALTY_CARD_AID.Length / 2);
                    var aidBytes = StringToByteArray(SAMPLE_LOYALTY_CARD_AID);
                    var command = SELECT_APDU_HEADER
                        .Concat(new byte[] { aidLength })
                        .Concat(aidBytes)
                        .ToArray();

                    var result = isoDep.Transceive(command);
                    var resultLength = result.Length;
                    byte[] statusWord = { result[resultLength - 2], result[resultLength - 1] };
                    var payload = new byte[resultLength - 2];
                    Array.Copy(result, payload, resultLength - 2);
                    var arrayEquals = SELECT_OK_SW.Length == statusWord.Length;

                    if (Enumerable.SequenceEqual(SELECT_OK_SW, statusWord))
                    {
                        var msg = Encoding.UTF8.GetString(payload);
                        await App.DisplayAlertAsync(msg);
                    }
                }
                catch (Exception e)
                {
                    await App.DisplayAlertAsync("Error communicating with card: " + e.Message);
                }
            }
        }

        public static byte[] StringToByteArray(string hex) => 
            Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
    }
}