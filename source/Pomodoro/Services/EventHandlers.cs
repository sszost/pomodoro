using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Services
{
   #region handlers

   public delegate void TimerUpdateEventHandler(object sender, TimerUpdateEventArgs e);
   public delegate void CountdownUpdateEventHandler(object sender, CountdownUpdateEventArgs e);
   public delegate void CountdownFinishEventHandler(object sender, EventArgs e);

   #endregion

   #region event args

   public class TimerUpdateEventArgs : EventArgs
   {
      private TimeSpan _newcurrenttime;

      public TimerUpdateEventArgs(TimeSpan newcurrenttime)
      {
         _newcurrenttime = newcurrenttime;
      }

      public TimeSpan NewCurrentTime
      {
         get
         {
            return _newcurrenttime;
         }
      }
   }

   public class CountdownUpdateEventArgs : EventArgs
   {
      private TimeSpan _newcountdowntime;

      public CountdownUpdateEventArgs(TimeSpan newcountdowntime)
      {
         _newcountdowntime = newcountdowntime;
      }

      public TimeSpan NewCountdownTime
      {
         get
         {
            return _newcountdowntime;
         }
      }
   }

   #endregion
}
