using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LhkEditor.Views
{
    public partial class BlargView : UserControl
    {
        public BlargView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}