using Pomodoro.Services;
using Pomodoro.ViewModels;
using Pomodoro.Views;
using Prism.Events;
using System.Globalization;
using System.Threading;
using System.Windows;
using Unity;

namespace Pomodoro
{
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : Application
   {
      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);

         // change culture so debugging messages are in english
         Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
         Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

         IUnityContainer container = new UnityContainer();
         //ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));

         container.RegisterType<IEventAggregator, EventAggregator>();
         container.RegisterType<ITimerService, TimerService>();
         container.RegisterType<ICountdownViewModel, CountdownViewModel>();
         container.RegisterType<IPomodoroViewModel, PomodoroViewModel>();

         var window = container.Resolve<MainWindow>();
         window.Show();
      }
   }
}