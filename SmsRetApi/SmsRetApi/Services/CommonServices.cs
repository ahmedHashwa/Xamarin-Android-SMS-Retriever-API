using Xamarin.Forms;

namespace SmsRetApi.Services
{
    public static class CommonServices
    {
        public static void ListenToSmsRetriever()
        {
            DependencyService.Get<IListenToSmsRetriever>()?.ListenToSmsRetriever();
        }
    }

 }