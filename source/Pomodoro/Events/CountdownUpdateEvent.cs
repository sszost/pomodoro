﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Events
{
   public class CountdownUpdateEvent : PubSubEvent<TimeSpan> { }
}
