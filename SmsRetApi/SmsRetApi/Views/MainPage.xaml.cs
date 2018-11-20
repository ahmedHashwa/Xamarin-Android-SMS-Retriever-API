using SmsRetApi.Models;
using SmsRetApi.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmsRetApi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.Subscribe<string>(Events.SmsRecieved, code =>
            {
                SmsCodeEntry.Text = code;
                _stopTimer = true;
            });
        }

        private bool _stopTimer;
        private TimeSpan _waitSpan;
        private void ImageButton_OnClicked(object sender, EventArgs e)
        {
             CommonServices.ListenToSmsRetriever();
            // View 
           _waitSpan = new TimeSpan(0, 5, 0);
            TimerLabel.TextColor = Color.Black;
            _stopTimer = false;
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
              {
                  if (_waitSpan.TotalSeconds == 0.0)
                      TimerLabel.TextColor = Color.Red;
                  TimerLabel.Text = _stopTimer ? "" : _waitSpan.ToString(@"mm\:ss");
                  _waitSpan = _waitSpan.Subtract(new TimeSpan(0, 0, 1));

                  return _waitSpan.TotalSeconds >= 0 && !_stopTimer;
              });
        }
    }
}