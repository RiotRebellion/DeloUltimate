using DeloUltimate.Presentation.ViewModels.Commands.Abstracts;
using System.Windows;

namespace DeloUltimate.Presentation.ViewModels.Commands
{
    public class ExpandApplicationCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Normal)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }
    }
}
