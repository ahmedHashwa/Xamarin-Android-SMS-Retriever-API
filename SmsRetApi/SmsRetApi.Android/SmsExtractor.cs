using System.Linq;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.Gms.Common.Apis;
using Com.Google.Android.Gms.Auth.Api.Phone;
using SmsRetApi.Models;
using SmsRetApi.Services;

namespace SmsRetApi.Droid
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { SmsRetriever.SmsRetrievedAction })]
    public class SmsReceiver : BroadcastReceiver
    {
     
        private static readonly string[] OtpMessageBodyKeywordSet = { "Verification Code" }; //You must define your own Keywords
        public override void OnReceive(Context context, Intent intent)
        {
            try
            {

                if (intent.Action != SmsRetriever.SmsRetrievedAction) return;
                var bundle = intent.Extras;
                if (bundle == null) return;
                var status = (Statuses)bundle.Get(SmsRetriever.ExtraStatus);
                switch (status.StatusCode)
                {
                    case CommonStatusCodes.Success:
                        // Get SMS message contents
                        var message = (string)bundle.Get(SmsRetriever.ExtraSmsMessage);
                        // Extract one-time code from the message and complete verification
                        // by sending the code back to your server.
                        var foundKeyword = OtpMessageBodyKeywordSet.Any(k => message.Contains(k));
                        if (!foundKeyword) return;
                        var code = ExtractNumber(message);
                        Utilities.Notify(Events.SmsRecieved, code);
                        break;
                    case CommonStatusCodes.Timeout:
                        // Waiting for SMS timed out (5 minutes)
                        // Handle the error ...
                        break;
                }
                //  ./keytool -alias androiddebugkey -exportcert -keystore g:/key/debug.keystore  -storepass android -keypass android | xxd -p | tr -d "[:space:]" | echo -n com.solonxpl.TravelerClubVisa `cat` | sha256sum | tr -d "[:space:]-" | xxd -r -p | base64 | cut -c1-11var pdus = bundle.Get("pdus");
            }
            catch (System.Exception)
            {
                // ignored
            }
        }
        private static string ExtractNumber(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            var number = Regex.Match(text, @"\d+").Value;
            return number;
        }
    }

}