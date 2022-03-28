using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LhkEditor.Views
{
    public partial class StoryProjectView : UserControl
    {
        public StoryProjectView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}