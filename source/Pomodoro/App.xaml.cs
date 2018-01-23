using Pomodoro.Models;
using Pomodoro.Services;
using Pomodoro.Views;
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

         container.RegisterType<ICountdownTimerService, CountdownTimerService>();
         container.RegisterType<ICountdownViewModel, CountdownViewModel>();
         container.RegisterType<CountdownView>();

         var window = container.Resolve<MainWindow>();
         window.Show();
      }
   }
}
