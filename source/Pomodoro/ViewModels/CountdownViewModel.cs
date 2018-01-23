using Pomodoro.Common;
using Pomodoro.Services;
using System;
using System.Windows.Input;
using Unity;

namespace Pomodoro.Models
{
   public interface ICountdownViewModel { }

   public class CountdownViewModel : ObservableObject, ICountdownViewModel
   {
      private ICountdownTimerService _countdownTimerWork;
      private TimeSpan _currentWorkTime;
      private TimeSpan _countdownWorkTime;

      private ICountdownTimerService _countdownTimerPlay;
      private TimeSpan _currentPlayTime;
      private TimeSpan _countdownPlayTime;

      public CountdownViewModel(IUnityContainer container)
      {
         _countdownTimerWork = container.Resolve<ICountdownTimerService>();
         _countdownTimerWork.Init(new TimeSpan(0, 20, 0),
            new TimerUpdateEventHandler(WorkTimerUpdate),
            new CountdownUpdateEventHandler(WorkCountdownUpdate),
            new CountdownFinishEventHandler(WorkCountdownFinish),
            new Uri(@"sounds/bell.wav", UriKind.Relative));

         _countdownTimerPlay = container.Resolve<ICountdownTimerService>();
         _countdownTimerPlay.Init(new TimeSpan(0, 20, 0),
            new TimerUpdateEventHandler(PlayTimerUpdate),
            new CountdownUpdateEventHandler(PlayCountdownUpdate),
            new CountdownFinishEventHandler(PlayCountdownFinish),
            new Uri(@"sounds/applause.wav", UriKind.Relative));
      }

      #region work

      public ICommand Restart
      {
         get { return new DelegateCommand(new Action(delegate { _countdownTimerWork.Restart(); })); }
      }

      public ICommand StartWork
      {
         get { return new DelegateCommand(new Action(delegate { _countdownTimerWork.Start(); })); }
      }

      public ICommand StopWork
      {
         get { return new DelegateCommand(new Action(delegate { _countdownTimerWork.Stop(); })); }
      }

      public ICommand UpWorkCountdown
      {
         get { return new DelegateCommand(new Action(delegate { _countdownTimerWork.UpCountdown(); })); }
      }

      public ICommand DownWorkCountdown
      {
         get { return new DelegateCommand(new Action(delegate { _countdownTimerWork.DownCountdown(); })); }
      }

      public TimeSpan CurrentWorkTime
      {
         get { return _currentWorkTime; }
         set
         {
            _currentWorkTime = value;
            OnPropertyChanged("CurrentWorkTime");
         }
      }

      private void WorkTimerUpdate(object sender, TimerUpdateEventArgs e)
      {
         CurrentWorkTime = e.NewCurrentTime;
      }

      public TimeSpan CountdownWorkTime
      {
         get { return _countdownWorkTime; }
         set
         {
            _countdownWorkTime = value;
            OnPropertyChanged("CountdownWorkTime");
         }
      }

      private void WorkCountdownUpdate(object sender, CountdownUpdateEventArgs e)
      {
         CountdownWorkTime = e.NewCountdownTime;
      }

      private void WorkCountdownFinish(object sender, EventArgs e)
      {
         _countdownTimerPlay.Start();
      }

      #endregion work

      #region play

      public ICommand StartPlay
      {
         get { return new DelegateCommand(new Action(delegate { _countdownTimerPlay.Start(); })); }
      }

      public ICommand StopPlay
      {
         get { return new DelegateCommand(new Action(delegate { _countdownTimerPlay.Stop(); })); }
      }

      public TimeSpan CurrentPlayTime
      {
         get { return _currentPlayTime; }
         set
         {
            _currentPlayTime = value;
            OnPropertyChanged("CurrentPlayTime");
         }
      }

      private void PlayTimerUpdate(object sender, TimerUpdateEventArgs e)
      {
         CurrentPlayTime = e.NewCurrentTime;
      }

      public TimeSpan CountdownPlayTime
      {
         get { return _countdownPlayTime; }
         set
         {
            _countdownPlayTime = value;
            OnPropertyChanged("CountdownPlayTime");
         }
      }

      private void PlayCountdownUpdate(object sender, CountdownUpdateEventArgs e)
      {
         CountdownPlayTime = e.NewCountdownTime;
      }

      private void PlayCountdownFinish(object sender, EventArgs e)
      {
      }

      #endregion play
   }
}