using Pomodoro.Common;
using System;
using System.Windows.Input;

namespace Pomodoro.ViewModels
{
   public interface IPomodoroViewModel { }

   public class PomodoroViewModel : IPomodoroViewModel
   {
      public ICommand RestartCommand { get; private set; }
      public ICommand ContinueCommand { get; private set; }

      public ICountdownViewModel WorkCountdownViewModel { get; set; }
      public ICountdownViewModel PlayCountdownViewModel { get; set; }

      public PomodoroViewModel(ICountdownViewModel work, ICountdownViewModel play)
      {
         WorkCountdownViewModel = work ?? throw new ArgumentNullException("work countdown view model");
         PlayCountdownViewModel = play ?? throw new ArgumentNullException("play countdown view model");

         WorkCountdownViewModel.Initialize(new TimeSpan(0, 20, 0), new Uri(@"sounds/bell.wav", UriKind.Relative));
         PlayCountdownViewModel.Initialize(new TimeSpan(0, 20, 0), new Uri(@"sounds/applause.wav", UriKind.Relative));

         RestartCommand = new DelegateCommand(OnRestart);
         ContinueCommand = new DelegateCommand(OnContinue);
      }

      private void OnContinue()
      {
         WorkCountdownViewModel.StopTimer();
         PlayCountdownViewModel.RestartTimer();
      }

      private void OnRestart()
      {
         WorkCountdownViewModel.RestartTimer();
         PlayCountdownViewModel.StopTimer();
      }

   }
}