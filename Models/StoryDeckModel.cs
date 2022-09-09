using System.Collections.ObjectModel;

namespace LhkEditor.Models;

public class StoryDeckModel
{
    public string DeckName { get; set; } = "Deck";        
    public ObservableCollection<StoryCardModel> Cards { get; }
        = new ObservableCollection<StoryCardModel>();
}