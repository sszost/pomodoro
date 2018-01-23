using Pomodoro.Models;
using System;
using System.Windows.Media;
using System.Windows.Threading;

namespace Pomodoro.Services
{
   public interface ICountdownTimerService
   {
      void Init(
         TimeSpan time,
         TimerUpdateEventHandler tu,
         CountdownUpdateEventHandler cu,
         CountdownFinishEventHandler cf,
         Uri sound);

      void Start();

      void Stop();

      void Restart();

      void Reset();

      void UpCountdown();

      void DownCountdown();
   }

   public class CountdownTimerService : ICountdownTimerService
   {
      private CountdownModel _countdownModel;
      private DispatcherTimer _dispacherTimer;
      private MediaPlayer _player;

      public event TimerUpdateEventHandler TimerUpdateEvent;

      public event CountdownUpdateEventHandler CountdownUpdateEvent;

      public event CountdownFinishEventHandler CountdownFinishEvent;

      public CountdownTimerService()
      {
         _dispacherTimer = new DispatcherTimer
         {
            Interval = new TimeSpan(0, 0, 1)
         };

         _dispacherTimer.Tick += new EventHandler(Tick);
         _player = new MediaPlayer();
      }

      public void Init(
         TimeSpan time,
         TimerUpdateEventHandler tu,
         CountdownUpdateEventHandler cu,
         CountdownFinishEventHandler cf,
         Uri sound)
      {
         _countdownModel = new CountdownModel(time);
         _player.Open(sound);

         TimerUpdateEvent += tu;
         CountdownUpdateEvent += cu;
         CountdownFinishEvent += cf;

         TimerUpdate();
         CountdownUpdate();
      }

      private void Tick(object sender, EventArgs e)
      {
         _countdownModel.CurrentTime = _countdownModel.CurrentTime.Subtract(new TimeSpan(0, 0, 1));
         TimerUpdate();
         if (_countdownModel.CurrentTime.Ticks == 0)
         {
            Stop();
            _player.Play();
            CountdownFinishEvent(this, new EventArgs());
         }
      }

      private void TimerUpdate()
      {
         TimerUpdateEvent(this, new TimerUpdateEventArgs(_countdownModel.CurrentTime));
      }

      private void CountdownUpdate()
      {
         CountdownUpdateEvent(this, new CountdownUpdateEventArgs(_countdownModel.CountdownTime));
      }

      public void Start()
      {
         _dispacherTimer.Start();
      }

      public void Stop()
      {
         _dispacherTimer.Stop();
      }

      public void Restart()
      {
         Reset();
         Start();
      }

      public void Reset()
      {
         Stop();
         _countdownModel.CurrentTime = _countdownModel.CountdownTime;
         TimerUpdate();
      }

      public void UpCountdown()
      {
         _countdownModel.CountdownTime = _countdownModel.CountdownTime.Add(new TimeSpan(0, 1, 0));
         CountdownUpdate();
      }

      public void DownCountdown()
      {
         if (_countdownModel.CountdownTime.Minutes > 1)
         {
            _countdownModel.CountdownTime = _countdownModel.CountdownTime.Subtract(new TimeSpan(0, 1, 0));
            CountdownUpdate();
         }
      }
   }
}