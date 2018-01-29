using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Services
{
   public interface ITimerService
   {
      void Initialize(TimeSpan time, Uri sound);

      void SubscribeOnTimerUpdateEvent(Action<TimeSpan> action);
      void SubscribeOnCountdownUpdateEvent(Action<TimeSpan> action);
      void SubscribeOnCountdownFinishEvent(Action action);

      void Start();

      void Stop();

      void Restart();

      void Reset();

      void UpCountdown();

      void DownCountdown();
   }
}
