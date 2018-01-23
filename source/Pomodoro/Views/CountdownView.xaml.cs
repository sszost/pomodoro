using Pomodoro.Models;
using System.Windows.Controls;

namespace Pomodoro.Views
{
   /// <summary>
   /// Interaction logic for Countdown.xaml
   /// </summary>
   public partial class CountdownView : UserControl
   {
      public CountdownView(ICountdownViewModel countdown)
      {
         InitializeComponent();
         DataContext = countdown;
      }
   }
}
