using System;
using System.Timers;
using Telerik.XamarinForms.ConversationalUI;
using Xamarin.Forms;

namespace SignalRChatDemo.CustomControls
{
    public class TimedChatEntry : ChatEntry, IDisposable
    {
        private Timer timer;

        public TimedChatEntry()
        {
            timer = new Timer(2000);
            timer.Elapsed += timer_Elapsed;

            TextChanged += TimedChatEntry_TextChanged;
        }

        public event EventHandler<EventArgs> TypingStarted;

        public event EventHandler<EventArgs> TypingEnded;

        private void TimedChatEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (timer == null)
            {
                return;
            }

            if (!timer.Enabled)
            {
                timer?.Start();
                Device.BeginInvokeOnMainThread(() => TypingStarted?.Invoke(this, new EventArgs()));
            }
            else
            {
                timer.Stop();
                timer.Start();
            }
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs args)
        {
            if (timer == null)
                return;

            if(timer.Enabled)
            {
                timer?.Stop();
                Device.BeginInvokeOnMainThread(() => TypingEnded?.Invoke(this, new EventArgs()));
            }
        }

        public void Dispose()
        {
            if (timer != null)
            {
                timer.Elapsed -= timer_Elapsed;
            }

            timer?.Dispose();
        }
    }
}
