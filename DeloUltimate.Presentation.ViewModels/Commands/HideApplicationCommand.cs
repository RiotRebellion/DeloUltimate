using DeloUltimate.Presentation.ViewModels.Commands.Abstracts;
using System.Windows;

namespace DeloUltimate.Presentation.ViewModels.Commands
{
    public class HideApplicationCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
    }
}
