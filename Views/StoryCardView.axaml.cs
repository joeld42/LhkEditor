using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LhkEditor.Views
{
    public partial class StoryCardView : UserControl
    {        
        public StoryCardView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}