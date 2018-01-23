using System.Windows;
using Unity;

namespace Pomodoro.Views
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public MainWindow(IUnityContainer container)
      {
         InitializeComponent();
         CountdownContent.Children.Add(container.Resolve<CountdownView>());
      }
   }
}
