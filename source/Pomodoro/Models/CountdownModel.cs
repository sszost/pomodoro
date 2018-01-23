using System;

namespace Pomodoro.Models
{
   public class CountdownModel
   {
      public CountdownModel(TimeSpan time)
      {
         CountdownTime = time;
         CurrentTime = time;
      }

      public TimeSpan CountdownTime { get; set; }
      public TimeSpan CurrentTime { get; set; }
   }
}