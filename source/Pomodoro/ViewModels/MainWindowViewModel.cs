using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.ViewModels
{
   public class MainWindowViewModel
   {
      public IPomodoroViewModel PomodoroViewModel { get; set; }
     
      public MainWindowViewModel(IPomodoroViewModel PomodoroViewModel)
      {
         this.PomodoroViewModel = PomodoroViewModel ?? throw new ArgumentNullException("pomodoro view model");
      }
   }
}
