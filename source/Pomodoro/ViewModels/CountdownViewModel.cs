using Pomodoro.Common;
using Pomodoro.Services;
using System;
using System.Windows.Input;

namespace Pomodoro.ViewModels
{
   public interface ICountdownViewModel
   {
      void Initialize(TimeSpan time, Uri uri);
      void RestartTimer();
      void StopTimer();
   }

   public class CountdownViewModel : ObservableObject, ICountdownViewModel
   {
      public ICommand UpCountdownCommand { get; private set; }
      public ICommand DownCountdownCommand { get; private set; }

      private ITimerService timer;
      private TimeSpan currentTime;
      private TimeSpan countdownTime;

      public CountdownViewModel(ITimerService work)
      {
         timer = work ?? throw new ArgumentNullException("work timer service");

         timer.SubscribeOnTimerUpdateEvent(OnTimerUpdate);
         timer.SubscribeOnCountdownUpdateEvent(OnCountdownUpdate);
         timer.SubscribeOnCountdownFinishEvent(OnCountdownFinish);

         UpCountdownCommand = new DelegateCommand(UpCountdown);
         DownCountdownCommand = new DelegateCommand(DownCountdown);
      }

      public void Initialize(TimeSpan time, Uri uri)
      {
         timer.Initialize(time, uri);
      }

      public void RestartTimer()
      {
         timer.Stop();
         timer.Restart();
      }

      public void StopTimer()
      {
         timer.Stop();
      }

      #region events

      private void OnTimerUpdate(TimeSpan time)
      {
         CurrentTime = time;
      }

      private void OnCountdownUpdate(TimeSpan time)
      {
         CountdownTime = time;
      }

      private void OnCountdownFinish()
      {
      }

      #endregion events

      #region commands

      public void UpCountdown()
      {
         timer.UpCountdown();
      }

      public void DownCountdown()
      {
         timer.DownCountdown();
      }

      #endregion commands

      #region properties

      public TimeSpan CurrentTime
      {
         get { return currentTime; }
         set
         {
            currentTime = value;
            OnPropertyChanged("CurrentTime");
         }
      }

      public TimeSpan CountdownTime
      {
         get { return countdownTime; }
         set
         {
            countdownTime = value;
            OnPropertyChanged("CountdownTime");
         }
      }

      #endregion properties
   }
}