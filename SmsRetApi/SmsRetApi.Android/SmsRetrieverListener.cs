using Android.Gms.Tasks;
using Com.Google.Android.Gms.Auth.Api.Phone;
using SmsRetApi.Services;
using Java.Lang;
 using SmsRetApi.Droid;
 using Xamarin.Forms;
 using Application = Android.App.Application;

[assembly: Dependency(typeof(ListenToSms))]

namespace SmsRetApi.Droid
{
    public class ListenToSms : IListenToSmsRetriever
    {
        public void ListenToSmsRetriever()
        {
            // Get an instance of SmsRetrieverClient, used to start listening for a matching
            // SMS message.
            SmsRetrieverClient client = SmsRetriever.GetClient(Application.Context);
            // Starts SmsRetriever, which waits for ONE matching SMS message until timeout
            // (5 minutes). The matching SMS message will be sent via a Broadcast Intent with
            // action SmsRetriever#SMS_RETRIEVED_ACTION.
            var task = client.StartSmsRetriever();
            // Listen for success/failure of the start Task. If in a background thread, this
            // can be made blocking using Tasks.await(task, [timeout]);
            task.AddOnSuccessListener(new SuccessListener());
            task.AddOnFailureListener(new FailureListener());
        }
        private class SuccessListener : Object, IOnSuccessListener
        {
            public void OnSuccess(Object result)
            {
            }
        }
        private class FailureListener : Object, IOnFailureListener
        {
            public void OnFailure(Exception e)
            {
            }
        }
    }

}