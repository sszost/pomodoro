using Pomodoro.ViewModels;
using System.Windows;
using Unity;

namespace Pomodoro.Views
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      private readonly MainWindowViewModel viewModel = null;

      public MainWindow(IUnityContainer container)
      {
         InitializeComponent();
         viewModel = container.Resolve<MainWindowViewModel>();
         DataContext = viewModel;
      }
   }
}
