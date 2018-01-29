using Pomodoro.Events;
using Pomodoro.Models;
using Prism.Events;
using System;
using System.Windows.Media;
using System.Windows.Threading;

namespace Pomodoro.Services
{
   public class TimerService : ITimerService
   {
      private IEventAggregator aggregator;

      private CountdownModel countdown;
      private DispatcherTimer timer;
      private MediaPlayer player;

      public TimerService(IEventAggregator aggregator)
      {
         this.aggregator = aggregator ?? throw new ArgumentNullException("event agreggator service");

         timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };

         timer.Tick += new EventHandler(OnTick);
         player = new MediaPlayer();
      }

      public void Initialize(TimeSpan time, Uri sound)
      {
         countdown = new CountdownModel(time);
         player.Open(sound);

         TimerUpdate();
         CountdownUpdate();
      }

      private void OnTick(object sender, EventArgs e)
      {
         countdown.CurrentTime = countdown.CurrentTime.Subtract(new TimeSpan(0, 0, 1));
         TimerUpdate();
         if (countdown.CurrentTime.Ticks == 0)
         {
            Stop();
            player.Play();
            aggregator.GetEvent<CountdownFinishEvent>().Publish();
         }
      }

      public void Start()
      {
         timer.Start();
      }

      public void Stop()
      {
         timer.Stop();
      }

      public void Restart()
      {
         Reset();
         Start();
      }

      public void Reset()
      {
         Stop();
         countdown.CurrentTime = countdown.CountdownTime;
         TimerUpdate();
      }

      public void UpCountdown()
      {
         countdown.CountdownTime = countdown.CountdownTime.Add(new TimeSpan(0, 1, 0));
         CountdownUpdate();
      }

      public void DownCountdown()
      {
         if (countdown.CountdownTime.Minutes > 1)
         {
            countdown.CountdownTime = countdown.CountdownTime.Subtract(new TimeSpan(0, 1, 0));
            CountdownUpdate();
         }
      }

      #region event publishers

      private void TimerUpdate()
      {
         aggregator.GetEvent<TimerUpdateEvent>().Publish(countdown.CurrentTime);
      }

      private void CountdownUpdate()
      {
         aggregator.GetEvent<CountdownUpdateEvent>().Publish(countdown.CountdownTime);
      }

      #endregion

      #region event subscriptions

      public void SubscribeOnTimerUpdateEvent(Action<TimeSpan> action)
      {
         aggregator.GetEvent<TimerUpdateEvent>().Subscribe(action);
      }

      public void SubscribeOnCountdownFinishEvent(Action action)
      {
         aggregator.GetEvent<CountdownFinishEvent>().Subscribe(action);
      }

      public void SubscribeOnCountdownUpdateEvent(Action<TimeSpan> action)
      {
         aggregator.GetEvent<CountdownUpdateEvent>().Subscribe(action);
      }

      #endregion
   }
}