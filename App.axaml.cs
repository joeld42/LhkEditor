using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LhkEditor.ViewModels;
using LhkEditor.Views;
using LhkEditor.Services;

namespace LhkEditor
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var db = new StoryDB();

                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel( db ),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}